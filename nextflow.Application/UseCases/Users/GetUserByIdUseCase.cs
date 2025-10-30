using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users;

public class GetUserByIdUseCase(IUserRepository repository)
    : GetByIdUseCaseBase<User, IUserRepository, UserResponseDto>(repository)
{
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);
}
