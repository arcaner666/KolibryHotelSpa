using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class PersonExtDtoLoginWithPhoneValidator : AbstractValidator<PersonExtDto>
{
    public PersonExtDtoLoginWithPhoneValidator()
    {
        RuleFor(p => p.Phone).NotEmpty();
        RuleFor(p => p.Password).NotEmpty();
        RuleFor(p => p.RefreshTokenDuration).NotEmpty();
    }
}
