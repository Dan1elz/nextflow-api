using Nextflow.Domain.Interfaces.Repositories.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> Login(string email, CancellationToken ct);
    Task<User?> GetByRefreshToken(string refreshToken, CancellationToken ct);
}