namespace People.API.Endpoints.People.Contracts;

public record UploadImageRequest(Guid Id, IFormFile File);
