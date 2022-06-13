using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class PersonClaim
    {
        public long PersonClaimId { get; set; }
        public long PersonId { get; set; }
        public int ClaimId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Claim Claim { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
