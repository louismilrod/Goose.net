using Goose.Models.Concert_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class ConcertsAttendedModel
    {
        public Guid UserId { get; set; }
        public int ConcertId { get; set; }
        public bool Attendance { get; set; }
        public List<ConcertListItem> ConcertListItems { get; set; }
    }
}
