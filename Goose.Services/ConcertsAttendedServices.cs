using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using Goose.Models.Concert_Models;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class ConcertsAttendedServices
    {
        private readonly Guid _userId;

        public ConcertsAttendedServices(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ConcertsAttendedModel> GetConcertsAttended()
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var concerts = ctx.Concerts;
                var query = ctx.ConcertsAttended.Select(a => new ConcertsAttendedModel
                {
                    UserId = a.UserId,
                    ConcertId = a.ConcertId,                    
                    Location = a.Concert.Location,
                    BandName = a.Concert.BandName,
                    VenueName = a.Concert.VenueName,
                    DateOfPerformance = a.Concert.PerformanceDate,
                    Notes = a.Concert.Notes,
                    InAttendance = true,
                    Setlists = a.Concert.Setlists.Select(b => new SetlistDataForConcertDetailView
                    {
                        SetNumber = b.SetNumber,

                        SongsForSetlist = b.SongsForSetList.Select(c=>new SongDetail
                        {
                            Title = c.Song.Title,
                            SongId = c.Song.SongId,
                        }).ToList(),
                    }).ToList()
                }).Where(e=>e.UserId == _userId).ToList();

                return query.ToList();
            }
        }

      

    }
}
