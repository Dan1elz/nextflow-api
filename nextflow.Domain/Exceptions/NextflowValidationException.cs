namespace Nextflow.Domain.Exceptions;

public class NextflowValidationException(IEnumerable<string> errors, string? message = "Ocorreram erros de validação.") : Exception(message)
{
    public IReadOnlyList<string> Errors { get; } = new List<string>(errors).AsReadOnly();
}