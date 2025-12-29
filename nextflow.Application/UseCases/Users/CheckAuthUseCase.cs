using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace Nextflow.Application.UseCases.Users;

public class CheckAuthUseCase(IUserRepository repository) : ICheckAuthUseCase
{
    private readonly IUserRepository _repository = repository;

    public async Task<LoginResponseDto> Execute(Guid userId, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(userId, ct)
            ?? throw new NotAuthorizedException("Usuário não encontrado.");

        if (!user.IsActive) throw new NotAuthorizedException("Usuário inativo.");
        
        return new LoginResponseDto
        {
            Token = string.Empty,
            UserId = user.Id,
            User = new UserResponseDto(user)
        };
    }
}

