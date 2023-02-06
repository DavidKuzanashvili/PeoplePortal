using People.Application.People;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal sealed class UploadImageEndpoint
{
    private static readonly string[] _allowedImageExtensions = new string[]
    {
        ".jpg", ".png", ".svg"
    };

    internal static async Task<IResult> ExecuteAsync(
        IFormFile file,
        Guid personId,
        IPeopleService peopleService,
        CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(file.FileName);

        if (!_allowedImageExtensions.Contains(extension!))
        {
            return Results.BadRequest();
        }
        var stream = file.OpenReadStream();
        var result = await peopleService.UploadImageAsync(
            Transform(personId, stream, extension), 
            cancellationToken);
        return Results.Ok(result);
    }

    internal static UploadImageDto Transform(Guid personId, Stream stream, string extension)
    {
        return new UploadImageDto(personId, stream, extension);
    }
}
