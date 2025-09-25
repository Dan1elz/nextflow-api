using nextflow.Application.Utils;
using nextflow.Domain.Exceptions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users.Token;

public class AuthenticationTokenUseCase(IUserRepository repository, JwtUtils jwtUtils) : IAuthenticationTokenUseCase
{
    private readonly IUserRepository _repository = repository;
    private readonly JwtUtils _jwtUtils = jwtUtils;
    public async Task<User> Execute(string token, CancellationToken ct)
    {
        var user = await _repository.GetAsync(u => u.RefreshToken == token, ct) ?? throw new NotAuthorizedException("Invalid token");
        var valid = _jwtUtils.ValidateToken(token);
        return !valid ? throw new NotAuthorizedException("Token expired, please log in again ") : user;
    }
}
