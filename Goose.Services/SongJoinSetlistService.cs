using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var query = ctx.SongsJoinSetlist.Select(s => new SongsJoinSetlistViewModel
                {
                    SongsJoinSetlistId = s.SongsJoinSetlistId,
                    SetlistId = s.SetlistId,
                    SongId = s.SongId,
                    PositionInSet = s.PositionInSet
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
                    PositionInSet = model.PositionInSet
                };

                ctx.SongsJoinSetlist.Add(joiningsongtosetlist);
                return ctx.SaveChanges() == 1;
            }
        }

        public SongsJoinSetlistViewModel GetSongsJoinSetlistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SongsJoinSetlist.Single(s => s.SetlistId == id);

                return new SongsJoinSetlistViewModel
                {
                    SetlistId = entity.SetlistId,
                    SongId = entity.SongId,
                    PositionInSet = entity.PositionInSet
                    
                };
            }
        }

        public bool UpdateSetlist(SongsJoinSetlistViewModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.SongsJoinSetlist.Single(s => s.SetlistId == model.SetlistId);

                entity.SetlistId = model.SetlistId;
                entity.SongId = model.SongId;
                entity.SongsJoinSetlistId = model.SongsJoinSetlistId;

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
    }
}
