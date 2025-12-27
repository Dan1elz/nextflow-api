using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Attributes;

public class AnoValidoAttribute(int minAno, int maxAno = -1) : ValidationAttribute
{
    public int MinAno { get; } = minAno;
    public int MaxAno { get; } = maxAno;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        if (value is not int ano)
            return new ValidationResult(
                "Valor inválido para o ano.",
                [validationContext.MemberName!]
            );

        int anoMaximo = MaxAno == -1
            ? DateTime.Now.Year + 1
            : MaxAno;

        if (ano < MinAno || ano > anoMaximo)
        {
            return new ValidationResult(
                $"Ano deve estar entre {MinAno} e {anoMaximo}.",
                [validationContext.MemberName!]
            );
        }

        return ValidationResult.Success!;
    }
}
