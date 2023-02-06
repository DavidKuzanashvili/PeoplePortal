using People.Application.Cities.Repositories;
using People.Application.Common;
using People.Application.Files;
using People.Application.Files.Models;
using People.Application.People.Dtos;
using People.Application.People.Repositories;
using People.Domain.Entities;
using People.Domain.Enums;

namespace People.Application.People;

public class PeopleService : IPeopleService
{
    private readonly string _filePath = "wwwroot/images";
    private readonly string _defaultImage = $"wwwroot/images/default-profile-pic.png";
    private readonly int _defaultSkip = 0;
    private readonly int _defaultTake = 10;
    private readonly SortingOrder _defaultSortingOrder = SortingOrder.Descending;
    private readonly IFileStorage _fileStorage;
    private readonly ICsvFileBuilder _csvFileBuilder;
    private readonly IPeopleRepository _peopleRepository;
    private readonly ICitiesRepository _citisRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PeopleService(
        IFileStorage fileStorage,
        ICsvFileBuilder csvFileBuilder,
        IPeopleRepository peopleRepository,
        ICitiesRepository citiesRepository,
        IUnitOfWork unitOfWork)
    {
        _fileStorage = fileStorage;
        _csvFileBuilder = csvFileBuilder;
        _peopleRepository = peopleRepository;
        _citisRepository = citiesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AssingRelatedPeopleAsync(
        AssignPeopleDto model, 
        CancellationToken cancellationToken)
    {
        var person = await _peopleRepository.GetAggregateAsync(model.Id, cancellationToken);
        if (person is null)
            throw new InvalidOperationException("Invalid operation: editing non exisiting person.");

        var relatedPersons = await _peopleRepository.GetByIdsReadonlyAsync(
            model.People.Where(x => x.Id != model.Id).Select(x => x.Id).ToList(),
            cancellationToken);

        person.ChangeRelatedPeople(relatedPersons
        .Select(x =>
        {
            var dto = model.People.Where(y => y.Id == x.EntityId).First();
            return RelatedPerson.Create(x.EntityId, dto.RelationType);
        }).ToList());

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<Guid> CreatePersonAsync(
        CreatePersonDto model, 
        CancellationToken cancellationToken)
    {
        var city = await _citisRepository.GetByNameAsync(model.CityName, cancellationToken);
        var person = Person.Create(
            model.Name,
            model.Surname,
            model.Gender,
            city!,
            model.PersonalNumber,
            model.DateOfBirth,
            _defaultImage,
            model.PhoneNumbers
                .Select(x => PhoneNumber.Create(
                    x.CountryCode, 
                    x.PhoneCode, 
                    x.Type)).ToList());

        _peopleRepository.Add(person);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return person.EntityId;
    }

    public async Task DeletePersonAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var person = await _peopleRepository.GetAggregateAsync(id, cancellationToken);
        _peopleRepository.Remove(person!);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<GetPersonDto?> GetByIdReadonlyAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var person = await _peopleRepository.GetByIdReadonlyAsync(id, cancellationToken);
        if (person is null) return null;
        return new GetPersonDto(
            person.EntityId,
            person.Name,
            person.Surname,
            person.Gender.ToString(),
            person.City.Name,
            person.PersonalNumber,
            person.DateOfBirth,
            person.ProfileImagePath,
            person.PhoneNumbers
                .Select(x => new PhoneNumberItemDto(x.Type.ToString(), x.CountryCode, x.PhoneCode))
                .ToList(),
            person.RelatedPeople
                .Select(x => new AssignedPersonItemDto(
                    x.RelatedPersonEntityId, 
                    x.RelationType.ToString()))
                .ToList());
    }

    public async Task<GetPeopleDto> GetFilteredReadonlyAsync(
        string? sort, 
        int? skip, 
        int? take,
        GetPeopleQueryDto query,
        CancellationToken cancellationToken)
    {

        var (people, totalCount) = await _peopleRepository.GetFilteredReadonlyAsync(
            sort is null ? _defaultSortingOrder : sort.GetSorting(),
            skip ?? _defaultSkip,
            take ?? _defaultTake,
            query,
            cancellationToken);

        return new GetPeopleDto(
            people
                .Select(x => new GetPeopleItemDto(
                    x.EntityId, 
                    x.Name, 
                    x.Surname, 
                    x.PersonalNumber))
                .ToList(), 
            totalCount);
    }

    public async Task<(byte[], string, string)> GetRelatedPeopleReportStreamAsync(
        CancellationToken cancellationToken)
    {
        var persons = await _peopleRepository.GetWithRelatedPeopleReadOnlyAsyn(cancellationToken);
        var records = persons
            .Select(x => new RelatedPeopleReportRecord()
            {
                Id = x.EntityId,
                Name = x.Name,
                PersonalNumber = x.PersonalNumber,
                Colleagues = x.RelatedPeople
                    .Count(c => c.RelationType == RelatedPersonType.Colleague),
                Relatives = x.RelatedPeople
                    .Count(c => c.RelationType == RelatedPersonType.Relative),
                Friends = x.RelatedPeople
                    .Count(c => c.RelationType == RelatedPersonType.Friend),
                Others = x.RelatedPeople
                    .Count(c => c.RelationType == RelatedPersonType.Other),
            })
            .ToList();
        var byteArr = _csvFileBuilder.BuildRelatedPeopleReportFile(records);
        return (byteArr, "csv", $"relation-report-{DateTime.UtcNow:dd-MM-YYYY}");
    }

    public async Task UpdatePersonAsync(
        UpdatePersonDto model, 
        CancellationToken cancellationToken)
    {
        var person = await _peopleRepository.GetAggregateAsync(model.Id, cancellationToken);
        if (person is null)
            throw new InvalidOperationException("Invalid operation: editing non exisiting person.");
        var city = await _citisRepository.GetByNameAsync(model.CityName, cancellationToken);
        person.Update(
            model.Name,
            model.Surname,
            model.Gender,
            city!,
            model.PersonalNumber,
            model.DateOfBirth,
            model.PhoneNumbers
                .Select(x => PhoneNumber.Create(x.CountryCode, x.PhoneCode, x.Type))
                .ToList());

        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<string> UploadImageAsync(
        UploadImageDto model, 
        CancellationToken cancellationToken)
    {
        var person = await GetByIdReadonlyAsync(model.Id, cancellationToken);
        if (person is null) return string.Empty;

        if (!person.ImagePath.Contains("default"))
            _fileStorage.Delete(person.ImagePath);

        string result = await _fileStorage.UploadAsync(
            $"{_filePath}/{person.PersonalNumber}-profile-pic{model.Extension}", 
            model.Stream,
            cancellationToken);
        await _peopleRepository.SetImagePathAsync(person.Id, result, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
