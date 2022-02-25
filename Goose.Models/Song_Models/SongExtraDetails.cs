using Goose.Data;
using Goose.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Song_Models
{
    public class SongExtraDetails
    {
        public DateTime DateOfPerformance { get; set; }
        public string Venue { get; set; }
        public string Locaton { get; set; }
        //public int Gap { get; set; }       
        public SetType Set { get; set; }        
        public SongListItem SongBefore { get; set; }
        public SongListItem SongAfter { get; set; }
        public int PositionInSet { get; set; }

    }
}
