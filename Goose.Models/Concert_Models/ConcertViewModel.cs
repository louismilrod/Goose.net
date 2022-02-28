using Goose.Data;
using Goose.Data.Data;
using Goose.Models.Setlist_Models;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Concert_Models
{
    public class ConcertViewModel
    {
        [Required]
        public int ConcertId { get; set; }
        [Required]
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        
        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Performance Date")]
        public DateTime DateOfPerformance { get; set; }       
        public string Notes { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        [Display(Name = "Setlist")]
        public List<SetlistViewModel> Setlists { get; set; }
    }
}
