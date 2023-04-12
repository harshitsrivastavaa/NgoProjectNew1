using System;
using System.Collections.Generic;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class NgoUserRole
    {
        public NgoUserRole()
        {
            NgoRegMembers = new HashSet<NgoRegMember>();
        }

        public int RoleId { get; set; }
        public string RoleType { get; set; }

        public virtual ICollection<NgoRegMember> NgoRegMembers { get; set; }
    }
}
