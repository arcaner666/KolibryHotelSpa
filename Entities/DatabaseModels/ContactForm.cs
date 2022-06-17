using System;
using System.Collections.Generic;

namespace Entities.DatabaseModels
{
    public partial class ContactForm
    {
        public long ContactFormId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
