using People.Application.Files;

namespace People.Infrastructure.Files;

public class FileStorage : IFileStorage
{
    public void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public async Task<string> UploadAsync(
        string path, 
        Stream stream,
        CancellationToken cancellationToken)
    {
        using var fStream = File.OpenWrite(path);
        await stream.CopyToAsync(fStream, cancellationToken);
        return path;
    }
}
