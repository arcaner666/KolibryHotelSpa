using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Invoices = new HashSet<Invoice>();
        }

        public byte PaymentTypeId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
