using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Data.Data
{
    public class SongsJoinSetlist
    {
        [Key]
        public int SongsJoinSetlistId { get; set; }
        [Required]
        public int PositionInSet { get; set; }
        [Required]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
        [Required]
        public int SetlistId { get; set; }
        public virtual Setlist Setlist { get; set; }
    }
}
