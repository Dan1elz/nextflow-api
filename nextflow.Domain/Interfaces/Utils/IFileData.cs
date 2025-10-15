namespace nextflow.Domain.Interfaces.Utils;

public interface IFileData
{
    string FileName { get; }
    string ContentType { get; }
    Stream OpenReadStream();
}