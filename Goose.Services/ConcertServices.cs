using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using Goose.Models.Concert_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class ConcertServices
    {
        private readonly Guid _userId;
        public ConcertServices()
        {

        }

        public ConcertServices(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ConcertSetlistListItem> GetConcertSetlist_List()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx.Concerts.Select(s => new ConcertSetlistListItem
                {
                    ConcertId = s.ConcertId,
                    BandName = s.BandName,
                    DateOfPerformance = s.PerformanceDate,
                    VenueName = s.VenueName,
                    Location = s.Location,
                    Sets = s.Setlists.Select(g=>g).ToList()
                    //Set_Two = s.Set_2.SongsForSetList.Select(g=>g.Song).ToList(),
                    //Set_Three = s.Set_3.SongsForSetList.Select(g=>g.Song).ToList(),
                    //Encore = s.Encore.SongsForSetList.Select(g=>g.Song).ToList(),
                    
                });

                return query.ToArray();
            }
        }

        public bool CreateConcert(ConcertViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var concert = new Concert()
                {
                    ConcertId = model.ConcertId,
                    BandName = model.BandName,
                    PerformanceDate = model.DateOfPerformance,
                    VenueName = model.VenueName,
                    Location = model.Location,                    
                    Notes = model.Notes,
                };

                ctx.Concerts.Add(concert);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
