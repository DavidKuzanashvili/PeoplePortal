using People.Application.Common;
using People.Application.People.Dtos;

namespace People.Application.People;

public interface IPeopleService
{
    Task AssingRelatedPeopleAsync(
        AssignPeopleDto model,
        CancellationToken cancellationToken);
   
    Task<Guid> CreatePersonAsync(
        CreatePersonDto model, 
        CancellationToken cancellationToken);

    Task<(byte[], string, string)> GetRelatedPeopleReportStreamAsync(
       CancellationToken cancellationToken);

    Task UpdatePersonAsync(
        UpdatePersonDto model, 
        CancellationToken cancellationToken);

    Task DeletePersonAsync(
        Guid id, 
        CancellationToken cancellationToken);

    Task<GetPersonDto?> GetByIdReadonlyAsync(
        Guid id, 
        CancellationToken cancellationToken);

    Task<GetPeopleDto> GetFilteredReadonlyAsync(
        string? sort,
        int? skip,
        int? take,
        GetPeopleQueryDto query,
        CancellationToken cancellationToken);

    Task<string> UploadImageAsync(
        UploadImageDto model, 
        CancellationToken cancellationToken);
}
