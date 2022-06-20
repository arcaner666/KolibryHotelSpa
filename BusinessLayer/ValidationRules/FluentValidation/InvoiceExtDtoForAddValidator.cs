using Entities.DTOs;
using Entities.ExtendedDatabaseModels;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation;

public class InvoiceExtDtoForAddValidator : AbstractValidator<InvoiceExtDto>
{
    public InvoiceExtDtoForAddValidator()
    {
        RuleFor(i => i.CurrencyId).NotEmpty();
        RuleFor(i => i.BuyerNameSurname).NotEmpty();
        RuleFor(i => i.BuyerEmail).NotEmpty();
        RuleFor(i => i.BuyerPhone).NotEmpty();
        RuleFor(i => i.ReservationStartDate).NotEmpty();
        RuleFor(i => i.ReservationEndDate).NotEmpty();
        RuleFor(i => i.Adult).NotEmpty();
        RuleFor(i => i.ChildAge1).NotEmpty().When(i => i.Child == 1);
        RuleFor(i => i.ChildAge2).NotEmpty().When(i => i.Child == 2);
        RuleFor(i => i.ChildAge3).NotEmpty().When(i => i.Child == 3);
        RuleFor(i => i.ChildAge4).NotEmpty().When(i => i.Child == 4);
        RuleFor(i => i.ChildAge5).NotEmpty().When(i => i.Child == 5);
        RuleFor(i => i.ChildAge6).NotEmpty().When(i => i.Child == 6);
        RuleFor(i => i.NetPrice).NotEmpty();
        RuleFor(i => i.Vat).NotEmpty();
        RuleFor(i => i.TotalVat).NotEmpty();
        RuleFor(i => i.TotalPrice).NotEmpty();
        RuleFor(i => i.InvoiceDetailDtos).NotEmpty();
    }
}
