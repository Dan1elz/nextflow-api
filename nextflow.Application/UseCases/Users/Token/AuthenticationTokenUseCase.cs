using Nextflow.Application.Utils;
using Nextflow.Domain.Exceptions;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users.Token;

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
