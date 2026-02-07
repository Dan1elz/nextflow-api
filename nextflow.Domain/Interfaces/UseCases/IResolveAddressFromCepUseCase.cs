using Nextflow.Domain.Dtos;

namespace Nextflow.Domain.Interfaces.UseCases;

public interface IResolveAddressFromCepUseCase
{
    Task<ResolveAddressFromCepResponseDto> Execute(
        ResolveAddressFromCepDto dto,
        CancellationToken ct
    );
}

