using Nextflow.Infrastructure.Database;
using Nextflow.Domain.Models;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Enums;

namespace Nextflow.Infrastructure.Seeders;

public class UsersSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Users.Any())
        {
            return;
        }

        var users = new User[]
        {
            new(new CreateUserDto(){
                Name = "Admin User",
                LastName = "Admin",
                Email = "admin@nextflow.com",
                CPF = "00000000000",
                Password = "!Admin123",
            })
            {
                Role = RoleEnum.Admin
            },
        };

        context.Users.AddRange(users);

        context.SaveChanges();
    }
}