using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Set1_Id { get; set; }
        public int? Set2_Id { get; set; }
        public int? Set3_Id { get; set; }
        public int? Encore_Id { get; set; }
        public string Notes { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime PerformanceDate { get; set; }        
        public string VenueName { get; set; }

        public virtual Setlist Set_1 { get; set; }
        public virtual Setlist Set_2 { get; set; }
        public virtual Setlist Set_3 { get; set; }
        public virtual Setlist Encore { get; set; }

        //public bool InAttendence { get; set; }

    }
}
