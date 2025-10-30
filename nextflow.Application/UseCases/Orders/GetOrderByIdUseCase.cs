using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases.Base;

namespace Nextflow.Application.UseCases.Orders;

public class GetOrderByIdUseCase(IOrderRepository repository)
    : IGetByIdUseCase<OrderResponseDto>
{
    private readonly IOrderRepository _repository = repository;
    public async Task<OrderResponseDto> Execute(Guid id, CancellationToken ct)
    {
        var entity = await _repository.GetByIdAsync(id, ct, x => x.Include(c => c.Client).Include(oi => oi.OrderItems).ThenInclude(p => p.Product))
            ?? throw new NotFoundException($"Pedido não encontrado com o Id: {id}");

        return new OrderResponseDto(entity);
    }

}