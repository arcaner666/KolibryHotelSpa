using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class ContactFormDtoForAddValidator : AbstractValidator<ContactFormDto>
{
    public ContactFormDtoForAddValidator()
    {
        RuleFor(c => c.NameSurname).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
    }
}
