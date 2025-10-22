using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Countries;

public class DeleteCountryUseCase(ICountryRepository repository)
    : DeleteUseCaseBase<Country, ICountryRepository>(repository)
{ }
