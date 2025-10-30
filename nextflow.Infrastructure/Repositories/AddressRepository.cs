using Nextflow.Infrastructure.Database;
using Nextflow.Infrastructure.Repositories.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Infrastructure.Repositories;

public class AddressRepository(AppDbContext context) : BaseRepository<Address>(context), IAddressRepository
{ }
