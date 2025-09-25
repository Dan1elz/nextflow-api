using nextflow.Application.Utils;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Interfaces.UseCases;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users.Token;

public class CreateTokenUseCase(IUserRepository repository, JwtUtils jwtUtils, IRevokeTokenUseCase revokeTokenUseCase) : ICreateTokenUseCase
{
    private readonly IUserRepository _repository = repository;
    private readonly JwtUtils _jwtUtils = jwtUtils;
    private readonly IRevokeTokenUseCase _revokeTokenUseCase = revokeTokenUseCase;
    public async Task<string?> Execute(User user, CancellationToken ct)
    {
        if (user.RefreshToken != null)
        {
            if (_jwtUtils.ValidateToken(user.RefreshToken))
                return user.RefreshToken;

            await _revokeTokenUseCase.Execute(user.Id, ct);
        }

        var newToken = _jwtUtils.GenerateToken(user);
        user.SetRefreshToken(newToken);
        await _repository.UpdateAsync(user, ct);

        return newToken;
    }
}
