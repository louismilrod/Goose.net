using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Data.Data
{
    public class Setlist
    {
        public enum SetType
        {
            SetOne = 1,
            SetTwo = 2,
            SetThree = 3,
            Encore = 4
        }

        [Key]
        public int SetlistId { get; set; }
        [Required]
        public SetType SetNumber { get; set; }

        public ICollection<SongsJoinSetlist> SongsForSetList { get; set; }
    }
}
