using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Application.Utils;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesController(
    ICreateSaleUseCase createUseCase,
    IDeleteSaleUseCase deleteUseCase,
    IGetAllUseCase<Sale, SaleResponseDto> getAllSalesUseCase,
    IGetByIdUseCase<SaleResponseDto> getSaleByIdUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto dto, CancellationToken ct)
    {
        dto.UserId = TokenHelper.GetUserId(this.User);
        var entity = await createUseCase.Execute(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {

        var userId = TokenHelper.GetUserId(this.User);
        await deleteUseCase.Execute(id, userId, ct);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        return Ok(new ApiResponse<ApiResponseTable<SaleResponseDto>>
        {
            Status = 200,
            Message = "Vendas recuperadas com sucesso.",
            Data = await getAllSalesUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<SaleResponseDto>
        {
            Status = 200,
            Message = "Venda recuperada com sucesso.",
            Data = await getSaleByIdUseCase.Execute(id, ct)
        });
    }
}