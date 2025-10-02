using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Countries;

public class CreateCountryUseCase(ICountryRepository repository)
    : CreateUseCaseBase<Country, ICountryRepository, CreateCountryDto, CountryResponseDto>(repository)
{
    protected override Country MapToEntity(CreateCountryDto dto) => new(dto);
    protected override CountryResponseDto MapToResponseDto(Country entity) => new(entity);
}
