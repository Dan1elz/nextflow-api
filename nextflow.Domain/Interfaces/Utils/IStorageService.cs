namespace nextflow.Domain.Interfaces.Utils;

public interface IStorageService
{
    public string BasePath { get; set; }
    Task<string> SaveAsync(IFileData file, CancellationToken ct);
    void DeleteAsync(string fileName);
    Task<byte[]> GetAsync(string fileName, CancellationToken ct);
}