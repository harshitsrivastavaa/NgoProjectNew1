using System;
using System.Collections.Generic;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class Cause
    {
        public int? MemberId { get; set; }
        public int CauseId { get; set; }
        public string RaiserName { get; set; }
        public string CauseName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Category { get; set; }
        public string Contact { get; set; }
        public string CauseDesc { get; set; }
        public string PickUpAddress { get; set; }
        public int? RaiserCount { get; set; }

        public virtual NgoRegMember Member { get; set; }
    }
}
