using Microsoft.EntityFrameworkCore;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;
using Nextflow.Infrastructure.Database;
using Nextflow.Infrastructure.Repositories.Base;

namespace Nextflow.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public virtual async Task<User?> Login(string email, CancellationToken ct)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public virtual async Task<User?> GetByRefreshToken(string refreshToken, CancellationToken ct)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, ct);
    }
}
