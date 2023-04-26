using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgoProjectNew1.Models.ViewModel
{
    public class CausesViewModel
    {
        public int? MemberId { get; set; }
        [Key]
        public int CauseId { get; set; }
        public string RaiserName { get; set; }
        public string CauseName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string Contact { get; set; }
        public string CauseDesc { get; set; }
        public string PickUpAddress { get; set; }
        public int? RaiserCount { get; set; }

        public class SearchResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // Add additional properties as needed
        }

    }
}
