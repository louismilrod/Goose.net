using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Song_Models
{
    public class SongListItem
    {
        public int SongId { get; set; }        
        public string Title { get; set; }       
        public string Artist { get; set; } 
        
        [Display(Name = "Original Artist")]
        public string OriginalArtist { get; set; }        

        [Display(Name = "Times Played")]
        public int TimesPlayed { get; set; }

        [Display(Name = "First Time Played")]
        public DateTime FirstTimePlayed { get; set; }

        [Display(Name = "Last Time Played")]
        public DateTime LastTimePlayed { get; set; }
        
    }
}
