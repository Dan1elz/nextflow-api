using System.ComponentModel.DataAnnotations;

namespace Nextflow.Domain.Attributes;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guid)
            return guid != Guid.Empty;

        if (value is Guid?)
        {
            var nullableGuid = (Guid?)value;
            return nullableGuid.HasValue && nullableGuid.Value != Guid.Empty;
        }

        return false;
    }
}


