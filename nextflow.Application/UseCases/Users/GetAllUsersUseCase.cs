using Nextflow.Application.Filters;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users;

public class GetAllUsersUseCase(IUserRepository repository)
    : GetAllUseCaseBase<User, IUserRepository, UserResponseDto>(repository)
{
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);

    protected override void ApplyFilters(FilterExpressionBuilder<User> builder, FilterSet filters)
    {
        builder
            .WhereStringContainsAny(filters, "search", u => u.Name, u => u.LastName, u => u.Email, u => u.CPF)
            .WhereStringContains(filters, "email", u => u.Email)
            .WhereStringContains(filters, "cpf", u => u.CPF);
    }
}