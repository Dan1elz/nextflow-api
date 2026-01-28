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
public class ContactsController(
    ICreateUseCase<CreateContactDto, ContactResponseDto> createUseCase,
    IUpdateUseCase<UpdateContactDto, ContactResponseDto> updateUseCase,
    IDeleteUseCase<Contact> deleteUseCase,
    IGetAllUseCase<Contact, ContactResponseDto> getAllContactsUseCase,
    IGetByIdUseCase<ContactResponseDto> getContactByIdUseCase
) : ControllerBase
{
    [HttpPost]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateContactDto dto, CancellationToken ct)
    {
        var createdContact = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, createdContact);
    }

    [HttpPut("{id:guid}")]
    [RoleAuthorize(RoleEnum.Admin)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContactDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<ContactResponseDto>
        {
            Status = 200,
            Message = "Contato atualizado com sucesso.",
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
        return Ok(new ApiResponse<ApiResponseTable<ContactResponseDto>>
        {
            Status = 200,
            Message = "Contatos encontrados com sucesso.",
            Data = await getAllContactsUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<ContactResponseDto>
        {
            Status = 200,
            Message = "Contato encontrado com sucesso.",
            Data = await getContactByIdUseCase.Execute(id, ct)
        });
    }
}
