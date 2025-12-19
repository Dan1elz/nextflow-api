namespace Nextflow.Domain.Exceptions;
public sealed class NextflowValidationException(IDictionary<string, string[]> errors, string? message = "Ocorreram erros de validação.") : Exception(message)
{
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>(errors);
}
