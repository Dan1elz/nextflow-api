using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nextflow.Application.Utils;
using nextflow.Attributes;
using nextflow.Domain.Dtos;
using nextflow.Domain.Enums;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Interfaces.UseCases.Base;
using nextflow.Domain.Models;

namespace nextflow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    ICreateUseCase<CreateUserDto, UserResponseDto> createUseCase,
    IUpdateUseCase<UpdateUserDto, UserResponseDto> updateUseCase,
    IDeleteUseCase deleteUseCase,
    IGetAllUseCase<User, UserResponseDto> getAllUsersUseCase,
    IGetByIdUseCase<UserResponseDto> getUserByIdUseCase,
    ILoginUseCase loginUseCase,
    IUpdatePasswordUseCase updateUserPasswordUseCase
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        var createdUser = await createUseCase.Execute(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<LoginResponseDto>
        {
            Status = 200,
            Message = "Login realizado com sucesso.",
            Data = await loginUseCase.Execute(dto, ct)
        });
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserDto dto, CancellationToken ct)
    {
        return Ok(new ApiResponse<UserResponseDto>
        {
            Status = 200,
            Message = "Usuário atualizado com sucesso.",
            Data = await updateUseCase.Execute(id, dto, ct)
        });
    }

    [Authorize]
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto, CancellationToken ct)
    {
        var userId = TokenHelper.GetUserId(this.User);
        await updateUserPasswordUseCase.Execute(userId, dto, ct);

        return Ok(new ApiResponseMessage
        {
            Status = 200,
            Message = "Senha atualizada com sucesso.",
        });
    }

    [Authorize]
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
        return Ok(new ApiResponse<ApiResponseTable<UserResponseDto>>
        {
            Status = 200,
            Message = "Usuários recuperados com sucesso.",
            Data = await getAllUsersUseCase.Execute(u => u.IsActive == true, offset, limit, ct)
        });
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        return Ok(new ApiResponse<UserResponseDto>
        {
            Status = 200,
            Message = "Usuário recuperado com sucesso.",
            Data = await getUserByIdUseCase.Execute(id, ct)
        });
    }
}