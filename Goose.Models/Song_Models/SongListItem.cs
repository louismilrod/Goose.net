using System;
using System.Collections.Generic;
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
        public string OriginalArtist { get; set; }
    }
}
