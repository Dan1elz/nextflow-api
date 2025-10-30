using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Sales;

public class DeleteSaleUseCase(
    ISaleRepository repository,
    IPaymentRepository paymentRepository,
    IUpdateStatusByOrderIdUseCase updateOrderStatusByOrderIdUseCase
) : IDeleteSaleUseCase
{
    private readonly ISaleRepository _repository = repository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUpdateStatusByOrderIdUseCase _updateOrderStatusByOrderIdUseCase = updateOrderStatusByOrderIdUseCase;

    public async Task Execute(Guid id, Guid userId, CancellationToken ct)
    {
        var sale = await _repository.GetAsync(x => x.Id == id, ct, x => x.Include(s => s.Payments))
            ?? throw new NotFoundException($"Venda não encontrada com o Id: {id}");

        if (!sale.IsActive)
            throw new BadRequestException("Venda já está cancelada.");

        sale.Delete();

        foreach (var p in sale.Payments)
            p.Delete();

        await _repository.UpdateAsync(sale, ct);
        await _paymentRepository.UpdateRangeAsync(sale.Payments, ct);
        await _updateOrderStatusByOrderIdUseCase.Execute(sale.OrderId, userId, OrderStatus.Returned, ct);
    }
}
