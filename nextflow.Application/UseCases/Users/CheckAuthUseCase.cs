using Nextflow.Application.Utils;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nextflow.Application.UseCases.Users;

public class CheckAuthUseCase(IUserRepository repository, JwtUtils jwtUtils) : ICheckAuthUseCase
{
    private readonly IUserRepository _repository = repository;
    private readonly JwtUtils _jwtUtils = jwtUtils;

    public async Task<LoginResponseDto> Execute(string token, CancellationToken ct)
    {
        // Valida o token
        if (!_jwtUtils.ValidateToken(token))
        {
            throw new NotAuthorizedException("Token inválido ou expirado. Por favor, faça login novamente.");
        }

        // Extrai o userId do token
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new NotAuthorizedException("Token inválido. ID do usuário não encontrado.");
        }

        // Busca o usuário
        var user = await _repository.GetByIdAsync(userId, ct)
            ?? throw new NotAuthorizedException("Usuário não encontrado.");

        // Verifica se o usuário está ativo
        if (!user.IsActive)
        {
            throw new NotAuthorizedException("Usuário inativo.");
        }

        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            User = new UserResponseDto(user)
        };
    }
}

