using Goose.Data;
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
                var query = ctx.ConcertsAttended.Where(x => x.UserId == _userId).Select(a => new ConcertsAttendedModel
                {
                    ConcertListItems = concerts.Select(b => new ConcertListItem
                    {
                        BandName = b.BandName,
                        DateOfPerformance = b.PerformanceDate,
                        VenueName = b.VenueName,
                        Location = b.Location,
                        Setlists = b.Setlists.Select(c => new SetlistDataForConcertDetailView
                        {
                            SetlistId = c.SetlistId,
                            SetNumber = c.SetNumber,
                            SongsForSetlist = c.SongsForSetList.Select(d => new SongDetail
                            {
                                Title = d.Song.Title,
                                SongId = d.Song.SongId

                            }).ToList()
                        }).ToList()


                    }).ToList()

                }).ToList();

                return query.ToList();
            }
        }


    }
}
