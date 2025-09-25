using nextflow.Application.UseCases.Base;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users;

public class GetAllUsersUseCase(IUserRepository repository)
    : GetAllUseCase<User, IUserRepository, UserResponseDto>(repository)
{
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);
}