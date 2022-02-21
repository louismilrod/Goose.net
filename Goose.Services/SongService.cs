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
        //        var nextof = ctx.Songs.Select(x => x.SongId);

        //        //OrderByDescending(a => a.SetlistId).GroupBy(b => b.PositionInSet).Select(c => c.Last());
        //        List<SongDetail> list = new List<SongDetail>();
        //        //find all the setlist w/ the song in it, loop through it
        //        foreach (var item in songsonsetlist)
        //        {
        //            if (item.SongId == songId)
        //            {
        //                SongDetail detail = new SongDetail()
        //                {
        //                    Title = item.Song.Title
        //                };

        //                list.Add(detail); //should now have a list of all the times the song occured on a setlist
        //            }
        //        }
        //        //list songs by position
        //        //????
        //        //get the song before/after in that set object

        //        var query = ctx.Songs.Select(s => new SongExtraDetails
        //        {
        //            DateOfPerformance = s.Concert.PerformanceDate,
        //            Venue = s.Concert.Venue,
        //            Gap = //number of occurances of a song in the db since last occurance of the song, look at the all the concerts by dateofperformance and count the performances inbetween or before
        //            Set = // s.setlist.setnumber
        //            SongBefore = before,
        //            SongAfter = occurance.
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
                    SongId = entity.SongId,
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
