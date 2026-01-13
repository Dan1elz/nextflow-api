namespace Nextflow.Domain.Interfaces.Models;

public interface IEntityMetadata
{
    bool IsActive { get; }
    string Preposition { get; }
    string Singular { get; }
    string Plural { get; }
}
