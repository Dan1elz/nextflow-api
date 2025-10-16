using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Cities;

public class UpdateCityUseCase(ICityRepository repository)
    : UpdateUseCaseBase<City, ICityRepository, UpdateCityDto, CityResponseDto>(repository)
{
    protected override CityResponseDto MapToResponseDto(City entity) => new(entity);
}
