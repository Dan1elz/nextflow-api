using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;
using Nextflow.Attributes;
using Nextflow.Domain.Enums;

namespace Nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AddressesController(
    ICreateUseCase<CreateAddressDto, AddressResponseDto> createUseCase,
    IUpdateUseCase<UpdateAddressDto, AddressResponseDto> updateUseCase,
    IDeleteUseCase<Address> deleteUseCase,
    IGetAllUseCase<Address, AddressResponseDto> getAllAddresssUseCase,
    IGetByIdUseCase<AddressResponseDto> getAddressByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateAddressDto dto, CancellationToken ct)
    {
        var createdAddress = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdAddress.Id }, createdAddress);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAddressDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<AddressResponseDto>
        {
            Status = 200,
            Message = "Endereço atualizado com sucesso.",
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
        return Ok(new ApiResponse<ApiResponseTable<AddressResponseDto>>
        {
            Status = 200,
            Message = "Endereços encontrados com sucesso.",
            Data = await getAllAddresssUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<AddressResponseDto>
        {
            Status = 200,
            Message = "Endereço encontrado com sucesso.",
            Data = await getAddressByIdUseCase.Execute(id, ct)
        });
    }
}
