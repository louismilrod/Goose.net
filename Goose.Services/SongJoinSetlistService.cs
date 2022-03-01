using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Goose.Services
{
    public class SongJoinSetlistService
    {
        private readonly Guid _userId;

        public SongJoinSetlistService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<SongsJoinSetlistViewModel> GetSongsJoinSetlist_List()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var titles = ctx.Songs.Select(x => x.Title).ToList();

                var query = ctx.SongsJoinSetlist.Select(s => new SongsJoinSetlistViewModel
                {
                    SongsJoinSetlistId = s.SongsJoinSetlistId,
                    SetlistId = s.SetlistId, 
                    DateOfPerformance = s.Setlist.Concert.PerformanceDate,
                    SetType = s.Setlist.SetNumber,
                    SongId = s.SongId,
                    Title = s.Song.Title,
                    PositionInSet = s.PositionInSet,
                });

                return query.ToArray();
            }
        }

        public bool CreateSongJoinSetlist(SongsJoinSetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var joiningsongtosetlist = new SongsJoinSetlist()
                {
                    
                    SetlistId = model.SetlistId,
                    SongId = model.SongId,
                    PositionInSet = model.PositionInSet,                 
                };

                ctx.SongsJoinSetlist.Add(joiningsongtosetlist);
                return ctx.SaveChanges() == 1;
            }
        }

        public SongsJoinSetlistViewModel GetSongsJoinSetlistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SongsJoinSetlist.Single(s => s.SongsJoinSetlistId == id);

                return new SongsJoinSetlistViewModel
                {
                    SongsJoinSetlistId = entity.SongsJoinSetlistId,
                    SetlistId = entity.SetlistId,
                    SongId = entity.SongId,
                    PositionInSet = entity.PositionInSet
                    
                };
            }
        }

        public bool UpdateSongJoinSetlist(SongsJoinSetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SongsJoinSetlist.Single(s => s.SongsJoinSetlistId == model.SongsJoinSetlistId);

                entity.SetlistId = model.SetlistId;
                entity.SongId = model.SongId;
                entity.PositionInSet = model.PositionInSet;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSongJoiningSetlist(int songjoinsetlistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SongsJoinSetlist.Single(s => s.SongsJoinSetlistId == songjoinsetlistId);

                ctx.SongsJoinSetlist.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public SelectList SelectListPopulator()
        {
            using (var ctx = new ApplicationDbContext())
            {
                SongsJoinSetlistViewModel model = new SongsJoinSetlistViewModel();
                var songs = ctx.Songs.ToList().OrderBy(x=>x.Title);
                model.SelectListSong = new SelectList(songs, "SongId", "Title");

                return model.SelectListSong;
            }          
        }

        public SelectList SelectListPopulatorEdit()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<int> positionsinset = new List<int>();
                for (int i = 1; i < 26; i++)
                {
                    positionsinset.Add(i);
                }
                SongsJoinSetlistViewModel model = new SongsJoinSetlistViewModel();
                model.SelectPositionInSet = new SelectList(positionsinset, "PositionInSet");
                return model.SelectPositionInSet;
            }
        }

        public SelectList SelectListPopulatorPositionInSet()
        {
            List<int> positionsinset = new List<int>();
            for (int i = 1; i < 26; i++)
            {
                positionsinset.Add(i);
            }
            SongsJoinSetlistViewModel model = new SongsJoinSetlistViewModel();
            model.SelectPositionInSet = new SelectList(positionsinset);
            return model.SelectPositionInSet;

        }
    }
}
