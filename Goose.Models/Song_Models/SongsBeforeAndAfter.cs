using Goose.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Song_Models
{
    public class SongsBeforeAndAfter
    {
        public SongListItem SongBefore { get; set; } = new SongListItem()
        {
            Title = "***"
        };
        public SongListItem SongAfter { get; set; } = new SongListItem()
        {
            Title = "***"
        };

    }
}
