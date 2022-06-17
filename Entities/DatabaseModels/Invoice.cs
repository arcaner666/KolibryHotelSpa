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
        public string BuyerNameSurname { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
        public string Title { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
        public bool Canceled { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual InvoiceType InvoiceType { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
