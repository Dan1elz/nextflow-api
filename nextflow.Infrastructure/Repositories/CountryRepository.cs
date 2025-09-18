using System.Linq.Expressions;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Infrastructure.Database;
using nextflow.Infrastructure.Repositories.Base;
using Nextflow.Domain.Models;

namespace nextflow.Infrastructure.Repositories;

public class CountryRepository(AppDbContext context) : BaseRepository<Country>(context), ICountryRepository
{ }