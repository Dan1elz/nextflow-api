using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Dtos;

public class ResolveAddressFromCepDto
{
    [StringLength(2)]
    public string? StateAcronym { get; set; }

    [StringLength(100)]
    public string? CityName { get; set; }

    [StringLength(7)]
    public string? CityIbgeCode { get; set; }
}

public class ResolveAddressFromCepResponseDto
{
    public Guid? StateId { get; set; }
    public string? StateName { get; set; }
    public string? StateAcronym { get; set; }

    public Guid? CityId { get; set; }
    public string? CityName { get; set; }
    public string? CityIbgeCode { get; set; }
}

