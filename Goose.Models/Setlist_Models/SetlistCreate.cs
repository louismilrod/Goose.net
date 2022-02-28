using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Goose.Data.Data.Setlist;

namespace Goose.Models.Setlist_Modles
{
    public class SetlistCreate
    {
        public int ConcertId { get; set; }
        [Display(Name = "Set Number")]
        public SetType SetNumber { get; set; }
    }
}
