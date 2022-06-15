using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class Person
    {
        public Person()
        {
            PersonClaims = new HashSet<PersonClaim>();
        }

        public long PersonId { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool Blocked { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual ICollection<PersonClaim> PersonClaims { get; set; }
    }
}
