using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users;

public class CreateUserUseCase(IUserRepository repository)
    : CreateUseCaseBase<User, IUserRepository, CreateUserDto, UserResponseDto>(repository)
{
    protected override User MapToEntity(CreateUserDto dto) => new(dto);
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);
}