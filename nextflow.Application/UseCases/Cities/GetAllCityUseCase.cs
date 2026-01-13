using Microsoft.EntityFrameworkCore;
using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Dtos;
using Nextflow.Domain.Models;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Application.UseCases.Cities;

public class GetAllCityUseCase(ICityRepository repository)
    : GetAllUseCaseBase<City, ICityRepository, CityResponseDto>(repository)
{
    protected override CityResponseDto MapToResponseDto(City entity) => new(entity);
    protected override Func<IQueryable<City>, IQueryable<City>>? GetInclude() => query => query.Include(city => city.State);
}