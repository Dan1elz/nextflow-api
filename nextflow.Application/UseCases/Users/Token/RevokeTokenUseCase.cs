using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Users.Token;

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
