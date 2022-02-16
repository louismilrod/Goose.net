using Goose.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Setlist_Models
{
    public class SetlistViewModel
    {
        public int SetlistId { get; set; }
        public SetType SetNumber {get; set;}        
        public virtual ICollection<SongsJoinSetlist> SongsForSetList { get; set; }
        public DateTime DateofPerformance { get; set; }

    }
}
