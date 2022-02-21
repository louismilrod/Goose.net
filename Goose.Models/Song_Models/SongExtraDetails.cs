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
        public int Gap { get; set; }
        public SetType Set { get; set; }
        public Song SongBefore { get; set; }
        public Song SongAfter { get; set; }
    }
}
