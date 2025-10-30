using Nextflow.Infrastructure.Database;
using Nextflow.Infrastructure.Repositories.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Infrastructure.Repositories;

public class CityRepository(AppDbContext context) : BaseRepository<City>(context), ICityRepository
{ }
