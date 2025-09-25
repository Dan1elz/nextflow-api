using nextflow.Application.UseCases.Base;
using nextflow.Domain.Dtos;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users;

public class CreateUserUseCase(IUserRepository repository)
    : CreateUseCaseBase<User, IUserRepository, CreateUserDto, UserResponseDto>(repository)
{
    protected override User MapToEntity(CreateUserDto dto) => new(dto);
    protected override UserResponseDto MapToResponseDto(User entity) => new(entity);
}