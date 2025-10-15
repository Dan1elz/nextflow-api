using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases.Base;

namespace nextflow.Application.UseCases.StockMovements;

public class DeleteStockMovementUseCase(IStockMovementRepository repository)
     : IDeleteUseCase
{
    protected readonly IStockMovementRepository _repository = repository;
    public async Task Execute(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Stock movement not found.");

        if (entity.IsActive && entity.CreateAt.AddHours(24) < DateTime.UtcNow)
            throw new BadRequestException("Não é possível excluir movimentações criadas há mais de 24 horas.");

        entity.Delete();

        await _repository.UpdateAsync(entity, ct);
    }
}
