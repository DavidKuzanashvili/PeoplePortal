namespace People.Application.People.Dtos;

public record UploadImageDto(Guid Id, Stream Stream, string Extension);
