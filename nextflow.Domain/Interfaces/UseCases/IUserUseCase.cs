using nextflow.Domain.Dtos;
using nextflow.Domain.Models;

namespace nextflow.Domain.Interfaces.UseCases;

public interface ILoginUseCase
{
    Task<LoginResponseDto> Execute(LoginDto dto, CancellationToken ct);
}
public interface IUpdatePasswordUseCase
{
    Task Execute(Guid userId, UpdatePasswordDto dto, CancellationToken ct);
}

public interface IAuthenticationTokenUseCase
{
    Task<User> Execute(string token, CancellationToken ct);
}

public interface ICreateTokenUseCase
{
    Task<string?> Execute(User user, CancellationToken ct);
}

public interface IRevokeTokenUseCase
{
    Task Execute(Guid id, CancellationToken ct);
}