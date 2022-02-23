using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Setlist_Modles
{
    public class SetlistCreate
    {
        public int ConcertId { get; set; }
        public SetType SetNumber { get; set; }
    }
}
