using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Setlist_Models
{
    public class SetlistViewModel
    {
        [Required]
        [Display(Name = "Setlist  Id")]
        public int SetlistId { get; set; }
        
        [Display(Name = "Concert Id")]
        public int ConcertId { get; set; }
        [Required]
        [Display(Name = "First(1) Second(2) Third Set(3) or Encore(4)")]
        public SetType SetNumber {get; set;}
        
        [Display(Name = "Date of Performance")]
        public DateTime DateOfPerformance { get; set; }        
        [Display(Name = "Songs In Performance")]

        public string Location { get; set; }
        public List<SongDetail> SongsForSetlist { get; set; }
        
    }
}
