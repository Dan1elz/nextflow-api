using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;
namespace Nextflow.Application.UseCases.Cities;

public class DeleteCityUseCase(ICityRepository repository)
: DeleteUseCaseBase<City, ICityRepository>(repository)
{ }
