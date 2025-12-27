using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace Nextflow.Application.UseCases.Users;

public class LoginUseCase(IUserRepository repository, ICreateTokenUseCase createTokenUseCase) : ILoginUseCase
{
    private readonly IUserRepository _repository = repository;
    private readonly ICreateTokenUseCase _createTokenUseCase = createTokenUseCase;
    public async Task<LoginResponseDto> Execute(LoginDto dto, CancellationToken ct)
    {
        var user = await _repository.Login(dto.Email, ct)
            ?? throw new BadRequestException("Email ou senha inválidos");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new BadRequestException("Email ou senha inválidos");

        var token = await _createTokenUseCase.Execute(user, ct)
            ?? throw new BadRequestException("Erro ao gerar token de acesso. Contacte o administrador do sistema.");

        return new LoginResponseDto
        {
            Token = token!,
            UserId = user.Id,
            User = new UserResponseDto(user)
        };
    }
}
