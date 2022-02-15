using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Song_Models
{
    public class SongCreate
    {      
        [MaxLength(150)]           
        public string Title { get; set; }
        public string Artist { get; set; }
       // public string Lyrics { get; set; }
        public string OriginalArtist { get; set; }
    }
}
