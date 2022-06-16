using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class InvoiceType
    {
        public InvoiceType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public byte InvoiceTypeId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
