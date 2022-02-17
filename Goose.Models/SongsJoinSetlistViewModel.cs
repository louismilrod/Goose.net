using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class SongsJoinSetlistViewModel
    {
        [Required]
        public int SongsJoinSetlistId { get; set; }
        [Required]
        public int SongId { get; set; }
        [Required]
        public int SetlistId { get; set; }
        [Required]
        public int PositionInSet { get; set; }
    }
}
