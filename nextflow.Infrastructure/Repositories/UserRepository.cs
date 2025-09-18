using Microsoft.EntityFrameworkCore;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;
using nextflow.Infrastructure.Database;
using nextflow.Infrastructure.Repositories.Base;

namespace nextflow.Infrastructure.Repositories;

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
