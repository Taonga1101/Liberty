using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class UserCredential
    {
        public int UserCredentialId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual User User { get; set; }
    }
}
