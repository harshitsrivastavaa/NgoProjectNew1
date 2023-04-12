using System;
using System.Collections.Generic;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class NgoNews
    {
        public int NewsId { get; set; }
        public string News { get; set; }
        public DateTime? StartDate { get; set; }
        public string Active { get; set; }
    }
}
