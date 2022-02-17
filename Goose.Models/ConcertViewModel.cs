using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class ConcertViewModel
    {
        [Required]
        public int ConcertId { get; set; }
        [Required]
        public string BandName { get; set; }
        [Required]
        public int Set1_Id { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime PerformanceDate { get; set; }
        public int? Set2_Id { get; set; }
        public int? Set3_Id { get; set; }
        public int? Encore_Id { get; set; }
        public string Notes { get; set; }
        public string VenueName { get; set; }
    }
}
