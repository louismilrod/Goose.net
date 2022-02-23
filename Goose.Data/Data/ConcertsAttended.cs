using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Data.Data
{
    public class ConcertsAttended
    {
        [Key]
        public int ConcertsAttendedId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int ConcertId { get; set; }
        public virtual Concert Concert { get; set; }
    }
}
