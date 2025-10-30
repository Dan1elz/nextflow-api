using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace Nextflow.Application.UseCases.StockMovements;

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

        if (entity.MovementType == MovementType.Sales)
            throw new BadRequestException("Não é possível excluir movimentações do tipo venda.");

        entity.Delete();

        await _repository.UpdateAsync(entity, ct);
    }
}
