using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class InvoiceDetail
    {
        public long InvoiceDetailId { get; set; }
        public long InvoiceId { get; set; }
        public int SuiteId { get; set; }
        public byte Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Suite Suite { get; set; }
    }
}
