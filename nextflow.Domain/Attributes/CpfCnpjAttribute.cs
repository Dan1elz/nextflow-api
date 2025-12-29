using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Attributes;

public class CpfCnpjAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success!;

        string documento = new([.. value.ToString()!.Where(char.IsDigit)]);

        string memberName = validationContext.MemberName!;

        if (documento.Length == 11)
        {
            return ValidarCpf(documento)
                ? ValidationResult.Success!
                : new ValidationResult(
                    "CPF inválido.",
                    [memberName]
                );
        }
        else if (documento.Length == 14)
        {
            return ValidarCnpj(documento)
                ? ValidationResult.Success!
                : new ValidationResult(
                    "CNPJ inválido.",
                    [memberName]
                );
        }
        else
        {
            return new ValidationResult(
                "Documento deve ser um CPF ou CNPJ válido.",
                [memberName]
            );
        }
    }

    private static bool ValidarCpf(string cpf)
    {
        if (cpf.All(c => c == cpf[0]))
            return false; // todos iguais

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    private static bool ValidarCnpj(string cnpj)
    {
        if (cnpj.All(c => c == cnpj[0]))
            return false; // todos iguais

        int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCnpj = cnpj[..12];
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
