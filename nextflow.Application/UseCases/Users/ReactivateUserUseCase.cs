using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Users;
public class ReactivateUserUseCase(IUserRepository repository) : IReactivateUserUseCase
{
    private readonly IUserRepository _repository = repository;
    public async Task<UserResponseDto> Execute(Guid userId, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(userId, ct);

        if (entity == null)
            throw new NotFoundException($"{entity?.Singular} com id {userId} não encontrad{entity?.Preposition}.");

        if (entity.IsActive)
            throw new BadRequestException($"{entity.Singular} não pode ser reativad{entity?.Preposition}");

        entity.Reactivate();

        await _repository.UpdateAsync(entity, ct);

        return new UserResponseDto(entity);
    }
}
