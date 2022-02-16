using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class SongsJoinSetlistViewModel
    {
        public int SongsJoinSetlistId { get; set; }
        public int SongId { get; set; }
        public int SetlistId { get; set; }
        public int PositionInSet { get; set; }
    }
}
