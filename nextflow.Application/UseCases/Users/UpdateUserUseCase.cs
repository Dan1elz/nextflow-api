using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users;

public class UpdateUserUseCase(IUserRepository repository)
    : UpdateUseCaseBase<User, IUserRepository, UpdateUserDto, UserResponseDto>(repository)
{
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);
}