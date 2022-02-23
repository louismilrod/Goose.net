using Goose.Data;
using Goose.Data.Data;
using Goose.Models.Setlist_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.Concert_Models
{
    public class ConcertListItem
    {
        public int ConcertId { get; set; }            
        public string Location { get; set; }
        public string BandName { get; set; }
        public string VenueName { get; set; }
        public DateTime DateOfPerformance { get; set; }
        public string Notes { get; set; }
        public List<SetlistDataForConcertDetailView> Setlists { get; set; }
    }
}
