using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Orders;

public class GetAllOrdersUseCase(IOrderRepository repository) : IGetAllUseCase<Order, OrderResponseDto>
{
    public async Task<ApiResponseTable<OrderResponseDto>> Execute(Expression<Func<Order, bool>> predicate, int offset, int limit, CancellationToken ct)
    {
        var data = await repository.GetAllAsync(predicate, offset, limit, ct, x => x.Include(c => c.Client).Include(oi => oi.OrderItems).ThenInclude(p => p.Product));
        var totalItems = await repository.CountAsync(predicate, ct);

        return new ApiResponseTable<OrderResponseDto>
        {
            Data = [.. data.Select(x => new OrderResponseDto(x))],
            TotalItems = totalItems
        };
    }
}