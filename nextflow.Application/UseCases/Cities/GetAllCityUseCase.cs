using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Cities;

public class GetAllCityUseCase(ICityRepository repository)
    : GetAllUseCaseBase<City, ICityRepository, CityResponseDto>(repository)
{
    protected override CityResponseDto MapToResponseDto(City entity) => new(entity);
}
