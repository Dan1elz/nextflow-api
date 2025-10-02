using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Countries;

public class GetCountryByIdUseCase(ICountryRepository repository)
    : GetByIdUseCaseBase<Country, ICountryRepository, CountryResponseDto>(repository)
{
    protected override CountryResponseDto MapToResponseDto(Country entity) => new(entity);
}
