using System;
using System.Collections.Generic;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class NgoRegMember
    {
        public NgoRegMember()
        {
            Causes = new HashSet<Cause>();
        }

        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public int? RoleId { get; set; }
        public string IsActive { get; set; }
        public string AdminComments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual NgoUserRole Role { get; set; }
        public virtual ICollection<Cause> Causes { get; set; }
    }
}
