using Nextflow.Application.Filters;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Clients;

public class GetAllClientUseCase(IClientRepository repository)
    : GetAllUseCaseBase<Client, IClientRepository, ClientResponseDto>(repository)
{
    protected override ClientResponseDto MapToResponseDto(Client entity) => new(entity);

    protected override void ApplyFilters(FilterExpressionBuilder<Client> builder, FilterSet filters)
    {
        builder
            .WhereStringContainsAny(filters, "search", c => c.Name, c => c.LastName, c => c.CPF)
            .WhereStringContains(filters, "cpf", c => c.CPF);
    }
}
