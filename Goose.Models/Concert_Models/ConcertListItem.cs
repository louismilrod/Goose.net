using Goose.Data;
using Goose.Data.Data;
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
        public List<Setlist> Sets { get; set; }         
        //public Setlist Set2 { get; set; }         
        //public Setlist Set3 { get; set; }         
        //public Setlist Encore { get; set; }         
        public string Location { get; set; }
        public string BandName { get; set; }
        public string VenueName { get; set; }
        public DateTime DateOfPerformance { get; set; }
        public string Notes { get; set; }

        public List<Song> Set1 { get; set; }
        //add a list of songs and pass into the controller


    }
}
