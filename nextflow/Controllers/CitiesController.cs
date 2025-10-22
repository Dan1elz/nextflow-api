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
public class CitiesController(
    ICreateUseCase<CreateCityDto, CityResponseDto> createUseCase,
    IUpdateUseCase<UpdateCityDto, CityResponseDto> updateUseCase,
    IDeleteUseCase deleteUseCase,
    IGetAllUseCase<City, CityResponseDto> getAllCitysUseCase,
    IGetByIdUseCase<CityResponseDto> getCityByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateCityDto dto, CancellationToken ct)
    {
        var createdCity = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, createdCity);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCityDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<CityResponseDto>
        {
            Status = 200,
            Message = "Cidade atualizada com sucesso.",
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
        return Ok(new ApiResponse<ApiResponseTable<CityResponseDto>>
        {
            Status = 200,
            Message = "Cidades recuperadas com sucesso.",
            Data = await getAllCitysUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<CityResponseDto>
        {
            Status = 200,
            Message = "Cidade recuperada com sucesso.",
            Data = await getCityByIdUseCase.Execute(id, ct)
        });
    }
}
