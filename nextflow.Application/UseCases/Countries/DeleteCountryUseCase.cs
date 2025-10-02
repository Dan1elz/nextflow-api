using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Countries;

public class DeleteCountryUseCase(ICountryRepository repository)
    : DeleteUseCaseBase<Country, ICountryRepository>(repository)
{ }
