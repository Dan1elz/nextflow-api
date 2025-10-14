using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;
using nextflow.Infrastructure.Database;
using nextflow.Infrastructure.Repositories.Base;

namespace nextflow.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{ }
