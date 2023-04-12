using System;
using System.Collections.Generic;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class NgoLogin
    {
        public int? MemberId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual NgoRegMember Member { get; set; }
    }
}
