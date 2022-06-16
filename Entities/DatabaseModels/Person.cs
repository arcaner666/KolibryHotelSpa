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
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public bool Blocked { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual ICollection<PersonClaim> PersonClaims { get; set; }
    }
}
