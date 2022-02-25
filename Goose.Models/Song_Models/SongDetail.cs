using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Song_Models
{
    public class SongDetail
    {
        public int SongId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        public string Lyrics { get; set; }
        public string OriginalArtist { get; set; }
        public int TimesPlayed { get; set; }
        public DateTime FirstTimePlayed { get; set; }
        public DateTime LastTimePlayed { get; set; }        
        public List<string> VenuesPerformedAt { get; set; }
        public double PercentageOfShows { get; set; }

    }
}
