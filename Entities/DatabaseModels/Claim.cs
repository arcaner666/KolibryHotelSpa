using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class Claim
    {
        public Claim()
        {
            PersonClaims = new HashSet<PersonClaim>();
        }

        public int ClaimId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<PersonClaim> PersonClaims { get; set; }
    }
}
