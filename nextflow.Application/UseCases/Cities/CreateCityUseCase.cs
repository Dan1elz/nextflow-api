using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Cities;

public class CreateCityUseCase(ICityRepository repository)
    : CreateUseCaseBase<City, ICityRepository, CreateCityDto, CityResponseDto>(repository)
{
    protected override City MapToEntity(CreateCityDto dto) => new(dto);
    protected override CityResponseDto MapToResponseDto(City entity) => new(entity);
}