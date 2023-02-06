namespace People.Application.Files;

public interface IFileStorage
{
    void Delete(string path);

    Task<string> UploadAsync(
        string path, 
        Stream stream, 
        CancellationToken cancellationToken);
}
