using Goose.Models.Concert_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class ConcertsAttendedModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int ConcertId { get; set; }
        public string Location { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        [Display(Name = "Date of Performance")]
        public DateTime DateOfPerformance { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Setlist")]
        public List<SetlistDataForConcertDetailView> Setlists { get; set; }
        public bool InAttendance { get; set; }        
    }  
}
