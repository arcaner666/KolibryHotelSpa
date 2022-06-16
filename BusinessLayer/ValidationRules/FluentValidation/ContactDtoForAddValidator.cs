using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class ContactDtoForAddValidator : AbstractValidator<ContactDto>
{
    public ContactDtoForAddValidator()
    {
        RuleFor(c => c.NameSurname).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Message).NotEmpty();
    }
}
