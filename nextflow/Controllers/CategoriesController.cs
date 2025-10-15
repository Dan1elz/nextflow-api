using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nextflow.Attributes;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Interfaces.UseCases.Base;
using nextflow.Domain.Models;

namespace nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(
    ICreateUseCase<CreateCategoryDto, CategoryResponseDto> createUseCase,
    IUpdateUseCase<UpdateCategoryDto, CategoryResponseDto> updateUseCase,
    IDeleteUseCase deleteUseCase,
    IGetAllUseCase<Category, CategoryResponseDto> getAllCategorysUseCase,
    IGetByIdUseCase<CategoryResponseDto> getCategoryByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken ct)
    {
        var entity = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<CategoryResponseDto>
        {
            Status = 200,
            Message = "Pais atualizado com sucesso.",
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
        return Ok(new ApiResponse<ApiResponseTable<CategoryResponseDto>>
        {
            Status = 200,
            Message = "Países recuperados com sucesso.",
            Data = await getAllCategorysUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<CategoryResponseDto>
        {
            Status = 200,
            Message = "País recuperado com sucesso.",
            Data = await getCategoryByIdUseCase.Execute(id, ct)
        });
    }
}