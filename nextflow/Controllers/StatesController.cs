using Microsoft.AspNetCore.Authorization;
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
public class StatesController(
    ICreateUseCase<CreateStateDto, StateResponseDto> createUseCase,
    IUpdateUseCase<UpdateStateDto, StateResponseDto> updateUseCase,
    IDeleteUseCase<State> deleteUseCase,
    IGetAllUseCase<State, StateResponseDto> getAllStatesUseCase,
    IGetByIdUseCase<StateResponseDto> getStateByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateStateDto dto, CancellationToken ct)
    {
        var entity = await createUseCase.Execute(dto, ct);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStateDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<StateResponseDto>
        {
            Status = 200,
            Message = "Estado atualizado com sucesso.",
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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int limit = 10, CancellationToken ct = default)
    {
        return Ok(new ApiResponse<ApiResponseTable<StateResponseDto>>
        {
            Status = 200,
            Message = "Estados encontrados com sucesso.",
            Data = await getAllStatesUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<StateResponseDto>
        {
            Status = 200,
            Message = "Estado encontrado com sucesso.",
            Data = await getStateByIdUseCase.Execute(id, ct)
        });
    }
}