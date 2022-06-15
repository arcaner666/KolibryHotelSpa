using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class PersonExtDtoRegisterValidator : AbstractValidator<PersonExtDto>
{
    public PersonExtDtoRegisterValidator()
    {
        RuleFor(p => p.Email).NotEmpty();
        RuleFor(p => p.Phone).NotEmpty();
        RuleFor(p => p.Password).NotEmpty();
    }
}
