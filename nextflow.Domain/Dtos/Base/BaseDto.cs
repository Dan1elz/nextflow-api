using System.ComponentModel.DataAnnotations;
using Nextflow.Domain.Exceptions;

namespace Nextflow.Domain.Dtos.Base;

public class BaseDto
{
    public void Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);

        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
        {
            var errors = validationResults
                .Where(vr => !string.IsNullOrWhiteSpace(vr.ErrorMessage))
                .Select(vr => vr.ErrorMessage!)
                .ToList();

            throw new NextflowValidationException(errors);
        }
    }
}
