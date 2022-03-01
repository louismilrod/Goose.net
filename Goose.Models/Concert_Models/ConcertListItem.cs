using Goose.Data;
using Goose.Data.Data;
using Goose.Models.Setlist_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Concert_Models
{
    public class ConcertListItem
    {
        public int ConcertId { get; set; }            
        public string Location { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        
        [Display(Name = "Performance Date")]
        public DateTime DateOfPerformance { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Setlist")]
        public List<SetlistDataForConcertDetailView> Setlists { get; set; }
        public bool InAttendance { get; set; }
    }
}
