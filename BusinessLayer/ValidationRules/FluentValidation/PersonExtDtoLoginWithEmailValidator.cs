using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class PersonExtDtoLoginWithEmailValidator : AbstractValidator<PersonExtDto>
{
    public PersonExtDtoLoginWithEmailValidator()
    {
        RuleFor(a => a.Email).NotEmpty();
        RuleFor(a => a.Password).NotEmpty();
    }
}
