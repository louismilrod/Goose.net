using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Data.Data
{
    public class Concert
    {
        [Key]
        public int ConcertId { get; set; }
        [Required]
        public string BandName { get; set; }
        [Required]
        public string Location { get; set; }
        public string VenueName { get; set; }

        [Required]
        public DateTime PerformanceDate { get; set; }        
        public string Notes { get; set; }     
        public ICollection<Setlist> Setlists { get; set; }
        
    }
}
