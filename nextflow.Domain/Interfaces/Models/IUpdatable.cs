

namespace Nextflow.Domain.Interfaces.Models;

public interface IUpdatable<in TRequestDto>
{
    void Update(TRequestDto dto);
}