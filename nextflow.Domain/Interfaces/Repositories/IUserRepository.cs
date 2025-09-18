using nextflow.Domain.Interfaces.Repositories.Base;
using nextflow.Domain.Models;

namespace nextflow.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> Login(string email, CancellationToken ct);
    Task<User?> GetByRefreshToken(string refreshToken, CancellationToken ct);
}