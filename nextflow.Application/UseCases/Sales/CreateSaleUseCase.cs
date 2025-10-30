using nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Sales;

public class CreateSaleUseCase(
    ISaleRepository repository,
    IPaymentRepository paymentRepository,
    IUpdateStatusByOrderIdUseCase updateOrderStatusByOrderIdUseCase,
    IGetByIdUseCase<OrderResponseDto> getOrderByIdUseCase

) : ICreateSaleUseCase
{
    private readonly ISaleRepository _repository = repository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUpdateStatusByOrderIdUseCase _updateOrderStatusByOrderIdUseCase = updateOrderStatusByOrderIdUseCase;
    private readonly IGetByIdUseCase<OrderResponseDto> _getOrderByIdUseCase = getOrderByIdUseCase;
    public async Task<SaleResponseDto> Execute(CreateSaleDto dto, CancellationToken ct)
    {
        dto.Validate();
        var order = await _getOrderByIdUseCase.Execute(dto.OrderId, ct);

        var existingSale = await _repository.GetAsync(x => x.OrderId == dto.OrderId && x.IsActive, ct);
        if (existingSale is not null)
            throw new BadRequestException("Já existe uma venda vinculada a este pedido.");

        var totalPayments = dto.Payments.Sum(p => p.Amount);
        if (totalPayments != order.TotalAmount)
            throw new BadRequestException($"O valor total dos pagamentos ({totalPayments:C}) não corresponde ao valor do pedido ({order.TotalAmount:C}).");

        Sale sale = new(dto);

        sale.Payments = [.. dto.Payments
            .Select(p =>
            {
                p.SaleId = sale.Id;
                return new Payment(p);
            })];

        await _repository.AddAsync(sale, ct);
        await _paymentRepository.AddRangeAsync(sale.Payments, ct);
        await _updateOrderStatusByOrderIdUseCase.Execute(order.Id, dto.UserId, OrderStatus.PaymentConfirmed, ct);

        return new SaleResponseDto(sale);
    }
}
