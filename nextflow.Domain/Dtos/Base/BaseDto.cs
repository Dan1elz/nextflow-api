using System.ComponentModel.DataAnnotations;
using System.Linq;
using Nextflow.Domain.Exceptions;

namespace Nextflow.Domain.Dtos.Base;

public class BaseDto
{
    public void Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);

        if (Validator.TryValidateObject(this, validationContext, validationResults, true))
            return;

        var errors = validationResults
            .Where(vr => !string.IsNullOrWhiteSpace(vr.ErrorMessage))
            .SelectMany(vr => vr.MemberNames.Select(member => new
            {
                Field = member,
                Message = vr.ErrorMessage!
            }))
            .GroupBy(x => x.Field)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Message).ToArray()
            );
        var allMessages = errors.Values.SelectMany(v => v).ToList();
        var message = allMessages.Any()
            ? string.Join(" e ", allMessages)
            : "Ocorreram erros de validação.";

        throw new NextflowValidationException(errors, message);
    }
}
