using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using Goose.Models.Concert_Models;
using Goose.Models.Setlist_Models;
using Goose.Models.Song_Models;
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

        public IEnumerable<ConcertListItem> GetConcert_List()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx.Concerts.Select(s => new ConcertListItem
                {
                    ConcertId = s.ConcertId,
                    BandName = s.BandName,
                    DateOfPerformance = s.PerformanceDate,
                    VenueName = s.VenueName,
                    Location = s.Location,   
                    Notes = s.Notes,
                    Setlists = s.Setlists.Select(a => new SetlistDataForConcertDetailView
                    {
                        SetlistId = a.SetlistId,
                        SetNumber = a.SetNumber,
                        SongsForSetlist = a.SongsForSetList.Select(b => new SongDetail
                        {
                            Title = b.Song.Title,
                            SongId = b.Song.SongId
                        }).ToList()
                    }).ToList(),


                    //maybe still need set one, set two

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

        public ConcertViewModelForConcertDetails GetConcertById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Concerts.Single(c => c.ConcertId == id);

                return new ConcertViewModelForConcertDetails
                {
                    ConcertId = entity.ConcertId,
                    BandName = entity.BandName,
                    Location = entity.Location,
                    DateOfPerformance = entity.PerformanceDate,
                    Notes = entity.Notes,
                    VenueName = entity.VenueName,
                    Setlists = entity.Setlists.Select(s => new SetlistDataForConcertDetailView
                    {
                        SetlistId = s.SetlistId,
                        SetNumber = s.SetNumber,
                        SongsForSetlist = s.SongsForSetList.Select(a => new SongDetail
                        {
                            Title = a.Song.Title
                        }).ToList()
                    }).ToList(),
                };
                    
                //    {
                //        SetlistId = s.SetlistId,
                //        SetNumber = s.SetNumber,
                //        SongsForSetlist = s.SongsForSetList.Select(a=>new SongDetail
                //        {
                //            Title = a.Song.Title,
                //            Artist = a.Song.Artist,
                //            OriginalArtist = a.Song.OriginalArtist,

                //        }).ToList()
                //    }).ToList(), 
                //};
            }
        }

        public bool UpdateConcert(ConcertViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Concerts.Single(c => c.ConcertId == model.ConcertId);

                    entity.BandName = model.BandName;
                    entity.PerformanceDate = model.DateOfPerformance;
                    entity.VenueName = model.VenueName;
                    entity.Location = model.Location;
                    entity.Notes = model.Notes;                    

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteConcert(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Concerts.Single(c => c.ConcertId == concertId);
                ctx.Concerts.Remove(entity);
                return ctx.SaveChanges() == 1;

            }


        }
    }
}
