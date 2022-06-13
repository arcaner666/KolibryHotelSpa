using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class PersonExtDtoLoginWithPhoneValidator : AbstractValidator<PersonExtDto>
{
    public PersonExtDtoLoginWithPhoneValidator()
    {
        RuleFor(a => a.Phone).NotEmpty();
        RuleFor(a => a.Password).NotEmpty();
    }
}
