using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Orders;

public class GetAllOrdersUseCase(IOrderRepository repository) : GetAllUseCaseBase<Order, IOrderRepository, OrderResponseDto>(repository)
{
    protected override OrderResponseDto MapToResponseDto(Order entity) => new(entity);
    protected override Func<IQueryable<Order>, IQueryable<Order>>? GetInclude() => query => query
            .Include(c => c.Client)
            .Include(oi => oi.OrderItems)
                .ThenInclude(p => p.Product);
}