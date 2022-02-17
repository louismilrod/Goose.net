using Goose.Data;
using Goose.Data.Data;
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
        [Required]
        [Display(Name = "Concert Id")]
        public int ConcertId { get; set; }
        [Required]
        [Display(Name = "Set Number")]
        public SetType SetNumber {get; set;}
        [Required]
        [Display(Name = "Date of Performance")]
        public DateTime DateOfPerformance { get; set; }        
        [Display(Name = "Songs In Performance")]
        public List<Song> SongsForSetlist { get; set; }

    }
}
