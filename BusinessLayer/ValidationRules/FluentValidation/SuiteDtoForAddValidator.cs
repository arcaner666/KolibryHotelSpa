using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class SuiteDtoForAddValidator : AbstractValidator<SuiteDto>
{
    public SuiteDtoForAddValidator()
    {
        RuleFor(s => s.Title).NotEmpty();
        RuleFor(s => s.Bed).NotEmpty();
        RuleFor(s => s.M2).NotEmpty();
        RuleFor(s => s.Price).NotEmpty();
        RuleFor(s => s.Vat).NotEmpty();
    }
}
