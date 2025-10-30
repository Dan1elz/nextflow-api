using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextflow.Application.Utils;
using Nextflow.Attributes;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController(
    ICreateOrderUseCase createUseCase,
    IUpdateUseCase<UpdateOrderDto, OrderResponseDto> updateUseCase,
    IDeleteOrderUseCase deleteUseCase,
    IGetAllUseCase<Order, OrderResponseDto> getAllOrdersUseCase,
    IGetByIdUseCase<OrderResponseDto> getOrderByIdUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
    {
        dto.UserId = TokenHelper.GetUserId(this.User);
        var entity = await createUseCase.Execute(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderDto dto, CancellationToken ct)
    {
        dto.UserId = TokenHelper.GetUserId(this.User);
        return Ok(new ApiResponse<OrderResponseDto>
        {
            Status = 200,
            Message = "Pedido atualizado com sucesso.",
            Data = await updateUseCase.Execute(id, dto, ct)
        });
    }

    [HttpDelete("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {

        var userId = TokenHelper.GetUserId(this.User);
        await deleteUseCase.Execute(id, userId, ct);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        return Ok(new ApiResponse<ApiResponseTable<OrderResponseDto>>
        {
            Status = 200,
            Message = "Pedidos recuperados com sucesso.",
            Data = await getAllOrdersUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<OrderResponseDto>
        {
            Status = 200,
            Message = "Pedido recuperado com sucesso.",
            Data = await getOrderByIdUseCase.Execute(id, ct)
        });
    }
}