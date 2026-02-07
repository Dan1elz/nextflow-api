using Nextflow.Domain.Dtos;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Interfaces.UseCases;

namespace Nextflow.Application.UseCases.Addresses;

public class ResolveAddressFromCepUseCase(
    IStateRepository stateRepository,
    ICityRepository cityRepository
) : IResolveAddressFromCepUseCase
{
    private readonly IStateRepository _stateRepository = stateRepository;
    private readonly ICityRepository _cityRepository = cityRepository;

    public async Task<ResolveAddressFromCepResponseDto> Execute(
        ResolveAddressFromCepDto dto,
        CancellationToken ct
    )
    {
        static string DigitsOnly(string value)
            => new(value.Where(char.IsDigit).ToArray());

        var uf = (dto.StateAcronym ?? string.Empty).Trim().ToUpperInvariant();
        var cityName = (dto.CityName ?? string.Empty).Trim();
        var cityIbge = DigitsOnly(dto.CityIbgeCode ?? string.Empty);

        // Preferência: resolver por IBGE (mais confiável que nome)
        if (cityIbge.Length == 7)
        {
            var cityByIbge = await _cityRepository.GetAsync(
                c => c.IsActive && c.IbgeCode == cityIbge,
                ct
            );

            if (cityByIbge is null)
                return new ResolveAddressFromCepResponseDto();

            var stateByCity = await _stateRepository.GetByIdAsync(cityByIbge.StateId, ct);
            if (stateByCity is null || !stateByCity.IsActive)
                return new ResolveAddressFromCepResponseDto();

            return new ResolveAddressFromCepResponseDto
            {
                StateId = stateByCity.Id,
                StateName = stateByCity.Name,
                StateAcronym = stateByCity.Acronym,
                CityId = cityByIbge.Id,
                CityName = cityByIbge.Name,
                CityIbgeCode = cityByIbge.IbgeCode
            };
        }

        // Fallback: UF + nome (quando IBGE não estiver disponível)
        if (uf.Length != 2 || cityName.Length < 2)
            return new ResolveAddressFromCepResponseDto();

        var state = await _stateRepository.GetAsync(
            s => s.IsActive && s.Acronym.ToUpper() == uf,
            ct
        );

        if (state is null)
        {
            return new ResolveAddressFromCepResponseDto();
        }

        var cityNameLower = cityName.ToLowerInvariant();
        var city = await _cityRepository.GetAsync(
            c =>
                c.IsActive &&
                c.StateId == state.Id &&
                c.Name.Equals(cityNameLower, StringComparison.CurrentCultureIgnoreCase),
            ct
        );

        return new ResolveAddressFromCepResponseDto
        {
            StateId = state.Id,
            StateName = state.Name,
            StateAcronym = state.Acronym,
            CityId = city?.Id,
            CityName = city?.Name,
            CityIbgeCode = city?.IbgeCode
        };
    }
}

