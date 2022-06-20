using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class Currency
    {
        public Currency()
        {
            Invoices = new HashSet<Invoice>();
        }

        public byte CurrencyId { get; set; }
        public string Title { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ExchangeRate { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
