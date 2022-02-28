using Goose.Models.Setlist_Models;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Concert_Models
{
    public class ConcertDetails
    { 
        public int ConcertId { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        public string Location { get; set; }
        [Display(Name = "Performance Date")]
        public DateTime DateOfPerformance { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        public List<SetlistDataForConcertDetailView> Setlists { get; set; }
     
    }

    public class SetlistDataForConcertDetailView
    {
        public int SetlistId { get; set; }
        [Display(Name = "Set Number")]
        public SetType SetNumber { get; set; }

        [Display(Name = "Setlist")]
        public List<SongDetail> SongsForSetlist { get; set; }
        
    }
}
