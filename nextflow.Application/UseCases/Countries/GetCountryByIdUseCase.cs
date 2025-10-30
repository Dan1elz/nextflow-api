using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Countries;

public class GetCountryByIdUseCase(ICountryRepository repository)
    : GetByIdUseCaseBase<Country, ICountryRepository, CountryResponseDto>(repository)
{
    protected override CountryResponseDto MapToResponseDto(Country entity) => new(entity);
}
