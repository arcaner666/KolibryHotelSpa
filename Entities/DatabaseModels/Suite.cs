using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class Suite
    {
        public Suite()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int SuiteId { get; set; }
        public string Title { get; set; } = null!;
        public byte Bed { get; set; }
        public short M2 { get; set; }
        public decimal Price { get; set; }
        public byte Vat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
