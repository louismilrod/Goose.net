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
        public string BandName { get; set; }
        
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime DateOfPerformance { get; set; }       
        public string Notes { get; set; }
        public string VenueName { get; set; }
        public List<SetlistViewModel> Setlists { get; set; }
        public List<SongDetail> Songs { get; set; }
    }
}
