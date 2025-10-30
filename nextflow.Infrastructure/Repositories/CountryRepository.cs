using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Infrastructure.Database;
using Nextflow.Infrastructure.Repositories.Base;
using Nextflow.Domain.Models;

namespace Nextflow.Infrastructure.Repositories;

public class CountryRepository(AppDbContext context) : BaseRepository<Country>(context), ICountryRepository
{ }