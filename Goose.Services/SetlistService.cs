using Goose.Data;
using Goose.Data.Data;
using Goose.Models.Setlist_Models;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class SetlistService
    {
        private readonly Guid _userId;

        public SetlistService()
        {

        }

        public SetlistService (Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<SetlistViewModel> GetSetlist_List()
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var query = ctx.Setlist.Select(s => new SetlistViewModel
                {
                    ConcertId = s.ConcertId,
                    SetlistId = s.SetlistId,
                    SetNumber = s.SetNumber,
                    SongsForSetlist = s.SongsForSetList.Select(g=> new SongDetail
                    {
                        Title = g.Song.Title,
                    }).ToList(), 
                    Location = s.Concert.Location,
                    DateOfPerformance = s.Concert.PerformanceDate
                }) ;

                return query.ToArray();
            }
        }

        public bool CreateSetlist(SetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var setlist = new Setlist()
                {
                    SetlistId= model.SetlistId,
                    ConcertId = model.ConcertId,                    
                    SetNumber = model.SetNumber,
                };                               

                ctx.Setlist.Add(setlist);
                return ctx.SaveChanges() == 1;
            }
        }

        public SetlistViewModel GetSetlistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Setlist.Find(id); 
                

                return new SetlistViewModel
                {
                    ConcertId = entity.ConcertId,
                    SetlistId = entity.SetlistId,
                    SetNumber = entity.SetNumber,
                    SongsForSetlist = entity.SongsForSetList.Select(s =>new SongDetail
                    {
                        Title = s.Song.Title
                    }).ToList(), //.select for list of songs
                    DateOfPerformance = entity.Concert.PerformanceDate
                };
            }
        }

        //public SetlistViewModel GetSetlistByDateTime(DateTime showtime)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity = ctx.Setlist.Single(s => s.DateofPerformance == showtime);

        //        return new SetlistViewModel
        //        {
        //            SetlistId = entity.SetlistId,
        //            SetNumber = entity.SetNumber,
        //            SongsForSetList = entity.SongsForSetList,
        //            DateofPerformance = entity.DateofPerformance
        //        };
        //    }
        //}

        public bool UpdateSetlist(SetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Setlist.Single(s => s.SetlistId == model.SetlistId);

                entity.ConcertId = model.ConcertId;
                entity.SetNumber = model.SetNumber;                

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSetlist(int setlistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Setlist
                        .Single(e => e.SetlistId == setlistId);

                ctx.Setlist.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
