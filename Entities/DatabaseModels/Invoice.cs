using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            Reservations = new HashSet<Reservation>();
        }

        public long InvoiceId { get; set; }
        public byte InvoiceTypeId { get; set; }
        public byte PaymentTypeId { get; set; }
        public byte CurrencyId { get; set; }
        public string BuyerNameSurname { get; set; } = null!;
        public string BuyerEmail { get; set; } = null!;
        public string BuyerPhone { get; set; } = null!;
        public string Title { get; set; } = null!;
        public decimal NetPrice { get; set; }
        public byte Vat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
        public bool Canceled { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Currency Currency { get; set; } = null!;
        public virtual InvoiceType InvoiceType { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
