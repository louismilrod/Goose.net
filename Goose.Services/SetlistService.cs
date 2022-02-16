using Goose.Data;
using Goose.Data.Data;
using Goose.Models.Setlist_Models;
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
                    SetlistId = s.SetlistId,
                    SetNumber = s.SetNumber,
                    SongsForSetList = s.SongsForSetList,
                    DateofPerformance = s.DateofPerformance
                });

                return query.ToArray();
            }
        }

        public bool CreateSetlist(SetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var setlist = new Setlist()
                {
                    SetNumber = model.SetNumber,
                    SongsForSetList = model.SongsForSetList,
                    DateofPerformance = model.DateofPerformance
                };

                ctx.Setlist.Add(setlist);
                return ctx.SaveChanges() == 1;
            }
        }

        public SetlistViewModel GetSetlistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Setlist.Single(s => s.SetlistId == id);

                return new SetlistViewModel
                {
                    SetlistId = entity.SetlistId,
                    SetNumber = entity.SetNumber,
                    SongsForSetList = entity.SongsForSetList,
                    DateofPerformance = entity.DateofPerformance                    
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

                entity.SetNumber = model.SetNumber;
                entity.SongsForSetList = model.SongsForSetList;
                entity.DateofPerformance = model.DateofPerformance;

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
