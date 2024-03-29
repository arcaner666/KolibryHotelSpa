﻿using Entities.DTOs;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class PersonExtDtoLoginWithEmailValidator : AbstractValidator<PersonExtDto>
{
    public PersonExtDtoLoginWithEmailValidator()
    {
        RuleFor(p => p.Email).NotEmpty();
        RuleFor(p => p.Password).NotEmpty();
        RuleFor(p => p.RefreshTokenDuration).NotEmpty();
    }
}
