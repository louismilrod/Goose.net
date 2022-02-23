using Goose.Data;
using Goose.Data.Data;
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

                //entity.SongId = model.SongId;
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


        public SongsBeforeAndAfter GetSongsBeforeAndAfter(int songId)
        {
            List<SongsBeforeAndAfter> allreturnedsongs = new List<SongsBeforeAndAfter>();

            using (var ctx = new ApplicationDbContext())
            {
                var songsonsetlist = ctx.SongsJoinSetlist;
                var setlist = ctx.Setlist;
                //find all the setlist w/ the song in it, loop through it
                foreach (var item in songsonsetlist)
                {
                    SongsBeforeAndAfter foreachloopreturnedsongs = new SongsBeforeAndAfter();

                    if (item.SongId == songId)
                    {
                        var setlistcontainingsong = setlist.Where(item2 => item2.SetlistId == item.SetlistId).FirstOrDefault();

                        if (setlistcontainingsong == null)
                        {
                            continue;
                        }
                        var querysubtractposition = setlistcontainingsong.SongsForSetList.FirstOrDefault(s => s.PositionInSet == --item.PositionInSet);
                        var queryaddposition = setlistcontainingsong.SongsForSetList.FirstOrDefault(s => s.PositionInSet == ++item.PositionInSet);
                        ///listitem
                        var songbefore = querysubtractposition.Song;
                        var songafter = queryaddposition.Song;

                        if (querysubtractposition != null)
                        {
                            var previoussong = new SongListItem
                            {
                                Title = songbefore.Title
                            };
                            foreachloopreturnedsongs.SongBefore = previoussong;
                        }

                        if (queryaddposition != null)
                        {
                            var nextsong = new SongListItem
                            {
                                Title = songafter.Title
                            };
                            foreachloopreturnedsongs.SongAfter = nextsong;
                        }

                        //populate the singular song before and after                        

                        //add item to list
                        allreturnedsongs.Add(foreachloopreturnedsongs);

                    };
                }
                return allreturnedsongs.FirstOrDefault();
                //to the list, selecting the first one


            }
        }

        //Gap = number of occurances of a song in the db since last occurance of the song, look at the all the concerts by dateofperformance and count the performances inbetween or before
        //public int ConcertsSinceSongOccurance(int songId)
        //{
        //    int gap;

        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var songtocount = ctx.Songs.Single(x => x.SongId == songId);
        //        var allconcert = ctx.Concerts.ToList();
        //        var concertcount = ctx.Concerts.Count();
        //        var sortedconcerts = allconcert.OrderBy(g => g.PerformanceDate).ToList();

        //        for (int i = 0; i < concertcount; i++)
        //        {
        //            Concert concert = sortedconcerts[i];  //get list of setlists and get songs from those
        //            var sortedsetlist = sortedconcerts.Select(a => a.Setlists).ToList();



        //            if (/*concert has the songs*/) //if the song is listed on a setlist in the concert then return how many counts its been
        //            {

        //                //stop counting and return the number of concerts it has been

        //                //after the first occurance of the concert now the count-restarts and we go until the next occurance of the song


        //                //keep current count and continue on with the count to find the next occurance of the song


        //                //if its the last occurance of the song then current count == # of concerts since last song event

        //                //add one concerts whenver i find one and skip to next loop and return the number

        //            }




        //        }
        //    }
        //    return gap;
        //}
    }
}
