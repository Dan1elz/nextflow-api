using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.StockMovements;

public class DeleteStockMovementUseCase(IStockMovementRepository repository)
     : DeleteUseCaseBase<StockMovement, IStockMovementRepository>(repository)
{
    protected override void ValidateBusinessRules(StockMovement entity)
    {
        if (entity.CreateAt.AddHours(24) < DateTime.UtcNow)
            throw new BadRequestException("Não é possível excluir movimentações criadas há mais de 24 horas.");

        if (entity.MovementType == MovementType.Sales)
            throw new BadRequestException("Não é possível excluir movimentações do tipo venda.");
    }
}