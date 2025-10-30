using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Countries;

public class CreateCountryUseCase(ICountryRepository repository)
    : CreateUseCaseBase<Country, ICountryRepository, CreateCountryDto, CountryResponseDto>(repository)
{
    protected override Country MapToEntity(CreateCountryDto dto) => new(dto);
    protected override CountryResponseDto MapToResponseDto(Country entity) => new(entity);
}
