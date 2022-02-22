using Goose.Data;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class SongService
    {
        private readonly Guid _userId;

        public SongService()
        {

        }
        public SongService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<SongListItem> GetSongLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Select(s => new SongListItem
                {
                    SongId = s.SongId,
                    Artist = s.Artist,
                    OriginalArtist = s.OriginalArtist,
                    Title = s.Title,                    
                });

                return query.ToArray();
            }
        }

        //public IEnumerable<SongExtraDetails> GetSongOccurances(int songId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var songsonsetlist = ctx.SongsJoinSetlist;
        //        var setlist = ctx.Setlist;
        //        //method to get song before
        //        //method to get song after

        //        List<SongExtraDetails> list = new List<SongExtraDetails>();
        //        //find all the setlist w/ the song in it, loop through it
        //        foreach (var item in songsonsetlist)
        //        {
        //            if (item.SongId == songId)
        //            {
        //                var setlistcontainingsong = setlist.Where(item2 => item2.SetlistId == item.SetlistId).FirstOrDefault();
        //                var querysubtractposition = setlistcontainingsong.SongsForSetList.Where(s => s.PositionInSet == item.PositionInSet--);
        //                //var queryaddposition = setlistcontainingsong.SongsForSetList.Select(s => s.PositionInSet == item.PositionInSet++);

        //                if (setlistcontainingsong == null)
        //                {
        //                    return null;
        //                }


        //                SongExtraDetails detail = new SongExtraDetails()
        //                {
        //                    SongAfter = querysubtractposition.Single(s => SongExtraDetails())

        //                };

        //                list.Add(detail);

        //            }


        //        }
        //        var sortedlist = list.OrderBy(songposition => songposition.PositionInSet);
        //        sortedlist..Where()
        //        //list songs by position
        //        //????
        //        //get the song before/after in that set object

        //        var query = ctx.Songs.Select(s => new SongExtraDetails
        //        {
        //            DateOfPerformance = s.SongJoinSetlist.Setlist.Concert.PerformanceDate,
        //            Venue = s.SongJoinSetlist.Setlist.Concert.VenueName,
        //            //Gap = number of occurances of a song in the db since last occurance of the song, look at the all the concerts by dateofperformance and count the performances inbetween or before
        //            Set = s.SongJoinSetlist.Setlist.SetNumber,
        //            SongBefore = before,
        //            SongAfter = occurance,

        //        });

        //        return query.ToArray();
        //    }
        //}


        public bool CreateSong(SongCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var song = new Song()
                {
                    Title = model.Title,
                    Artist = model.Artist,
                    OriginalArtist = model.OriginalArtist
                };

                ctx.Songs.Add(song);
                return ctx.SaveChanges() == 1;
            }
        }

        public SongDetail GetSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Single(s=>s.SongId == id);

                return new SongDetail
                {
                    //SongId = entity.SongId,
                    Title = entity.Title,
                    Artist = entity.Artist,
                    OriginalArtist = entity.OriginalArtist,
                    Lyrics = entity.Lyrics,
                    //TimesPlayed = ctx.SongsJoinSetlist.Where(f=>f.SongId == id).Count()
                };
            }
        }

        public bool UpdateSong(SongEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Single(s => s.SongId == model.SongId);

                
                entity.Title = model.Title;
                entity.Artist = model.Artist;
                entity.OriginalArtist = model.OriginalArtist;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSong(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == songId);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
