using AutoMapper;
using Entities.DatabaseModels;
using Entities.DTOs;
using Entities.ExtendedDatabaseModels;

namespace BusinessLayer.CrossCuttingConcerns.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactForm, ContactFormDto>();
        CreateMap<ContactForm, ContactFormDto>().ReverseMap();
        CreateMap<Claim, ClaimDto>();
        CreateMap<Claim, ClaimDto>().ReverseMap();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<Currency, CurrencyDto>().ReverseMap();
        CreateMap<Person, PersonDto>();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<PersonClaim, PersonClaimDto>();
        CreateMap<PersonClaim, PersonClaimDto>().ReverseMap();
        CreateMap<PersonClaimExt, PersonClaimExtDto>();
        CreateMap<PersonClaimExt, PersonClaimExtDto>().ReverseMap();
        CreateMap<PersonDto, PersonExtDto>();
        CreateMap<PersonDto, PersonExtDto>().ReverseMap();
        CreateMap<PersonExt, PersonExtDto>();
        CreateMap<PersonExt, PersonExtDto>().ReverseMap();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<InvoiceDetail, InvoiceDetailDto>();
        CreateMap<InvoiceDetail, InvoiceDetailDto>().ReverseMap();
        CreateMap<InvoiceDto, InvoiceExtDto>();
        CreateMap<InvoiceDto, InvoiceExtDto>().ReverseMap();
        CreateMap<InvoiceExt, InvoiceExtDto>();
        CreateMap<InvoiceExt, InvoiceExtDto>().ReverseMap();
        CreateMap<Suite, SuiteDto>();
        CreateMap<Suite, SuiteDto>().ReverseMap();
    }
}
