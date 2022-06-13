using FluentValidation.Results;

namespace BusinessLayer.Extensions;

public class ValidationErrorDetails
{
    public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
}
