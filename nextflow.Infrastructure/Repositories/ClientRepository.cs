using nextflow.Infrastructure.Database;
using nextflow.Infrastructure.Repositories.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : BaseRepository<Client>(context), IClientRepository
{
}
