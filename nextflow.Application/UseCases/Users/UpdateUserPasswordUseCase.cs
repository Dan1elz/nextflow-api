using nextflow.Domain.Dtos;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Users;

public class UpdateUserPasswordUseCase(IUserRepository repository, IRevokeTokenUseCase revokeTokenUseCase)
    : IUpdatePasswordUseCase
{
    private readonly IUserRepository _repository = repository;
    private readonly IRevokeTokenUseCase _revokeTokenUseCase = revokeTokenUseCase;
    public async Task Execute(Guid userId, UpdatePasswordDto dto, CancellationToken ct)
    {
        dto.Validate();
        var entity = await _repository.GetByIdAsync(userId, ct)
            ?? throw new NotFoundException($"User com id {userId} não encontrado.");

        entity.UpdatePassword(dto);

        await _revokeTokenUseCase.Execute(userId, ct);
        await _repository.UpdateAsync(entity, ct);
    }
}
