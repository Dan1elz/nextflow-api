using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextflow.Attributes;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Controllers;

public class SuppliersController(
    ICreateUseCase<CreateSupplierDto, SupplierResponseDto> createUseCase,
    IUpdateUseCase<UpdateSupplierDto, SupplierResponseDto> updateUseCase,
    IDeleteUseCase deleteUseCase,
    IGetAllUseCase<Supplier, SupplierResponseDto> getAllSuppliersUseCase,
    IGetByIdUseCase<SupplierResponseDto> getSupplierByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto, CancellationToken ct)
    {
        var createdSupplier = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSupplierDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<SupplierResponseDto>
        {
            Status = 200,
            Message = "Fornecedor atualizado com sucesso.",
            Data = await updateUseCase.Execute(id, dto, ct)
        });
    }

    [HttpDelete("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await deleteUseCase.Execute(id, ct);

        return NoContent();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        return Ok(new ApiResponse<ApiResponseTable<SupplierResponseDto>>
        {
            Status = 200,
            Message = "Fornecedor recuperados com sucesso.",
            Data = await getAllSuppliersUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<SupplierResponseDto>
        {
            Status = 200,
            Message = "Fornecedor recuperado com sucesso.",
            Data = await getSupplierByIdUseCase.Execute(id, ct)
        });
    }
}


