using nextflow.Domain.Dtos;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;

namespace nextflow.Application.UseCases.Users;

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
        };
    }
}
