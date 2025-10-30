using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace Nextflow.Application.UseCases.Users.Token;

public class RevokeTokenUseCase(IUserRepository repository) : IRevokeTokenUseCase
{
    private readonly IUserRepository _repository = repository;

    public async Task Execute(Guid userId, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(userId, ct)
            ?? throw new NotFoundException($"User com id {userId} não encontrado.");

        user.RevokeRefreshToken();
        await _repository.UpdateAsync(user, ct);
    }
}
