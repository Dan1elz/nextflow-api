﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nextflow.Attributes;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.UseCases.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClientsController(
    ICreateUseCase<CreateClientDto, ClientResponseDto> createUseCase,
    IUpdateUseCase<UpdateClientDto, ClientResponseDto> updateUseCase,
    IDeleteUseCase deleteUseCase,
    IGetAllUseCase<Client, ClientResponseDto> getAllClientsUseCase,
    IGetByIdUseCase<ClientResponseDto> getClientByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto, CancellationToken ct)
    {
        var createdClient = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClientDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<ClientResponseDto>
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
        return Ok(new ApiResponse<ApiResponseTable<ClientResponseDto>>
        {
            Status = 200,
            Message = "Endereços recuperados com sucesso.",
            Data = await getAllClientsUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<ClientResponseDto>
        {
            Status = 200,
            Message = "Endereço recuperado com sucesso.",
            Data = await getClientByIdUseCase.Execute(id, ct)
        });
    }
}

