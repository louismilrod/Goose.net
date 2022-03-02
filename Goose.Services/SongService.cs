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
                var list = new List<SongListItem>();
                foreach (var item in ctx.Songs)
                {
                    var entity = ctx.Songs.Single(s => s.SongId == item.SongId);
                    double timesplayed = ctx.SongsJoinSetlist.Where(f => f.SongId == item.SongId).Count();
                    double totalshows = ctx.Concerts.Count();
                    double averagetoround = timesplayed / totalshows;
                    var avgtimesplayed = Math.Round(averagetoround, 3);

                    var item2 = new SongListItem()
                    {
                        SongId = item.SongId,
                        Artist = item.Artist,
                        OriginalArtist = item.OriginalArtist,
                        Title = item.Title,
                        FirstTimePlayed = FirstTimePlayed(item.SongId),
                        LastTimePlayed = LastTimePlayed(item.SongId),
                        TimesPlayed = ctx.SongsJoinSetlist.Where(f => f.SongId == item.SongId).Count(),
                        AvgTimesPlayed = avgtimesplayed
                    };

                    list.Add(item2);
                }
                return list;
               
            }
        }
        public bool CreateSong(SongCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var song = new Song()
                {
                    Title = model.Title,
                    Artist = "Goose",//model.Artist,
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
                var entity = ctx.Songs.Single(s => s.SongId == id);
                double timesplayed = ctx.SongsJoinSetlist.Where(f => f.SongId == id).Count();
                double totalshows = ctx.Concerts.Count();

                return new SongDetail
                {
                    SongId = entity.SongId,
                    Title = entity.Title,
                    Artist = entity.Artist,
                    OriginalArtist = entity.OriginalArtist,
                    FirstTimePlayed = FirstTimePlayed(id),
                    LastTimePlayed = LastTimePlayed(id),
                    TimesPlayed = ctx.SongsJoinSetlist.Where(f => f.SongId == id).Count(),
                    PercentageOfShows = timesplayed/totalshows,
                    
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

        public DateTime FirstTimePlayed(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var songtocount = ctx.Songs.Single(x => x.SongId == songId);
                var concertswithsong = songtocount.SongJoinSetlists.Select(x => x.Setlist.Concert); 
                var concertsinorder = concertswithsong.OrderBy(s => s.PerformanceDate);
                var firstconcertperformance = concertsinorder.FirstOrDefault();
                if (firstconcertperformance != null)
                {
                    return firstconcertperformance.PerformanceDate;
                }

                else
                {
                    return DateTime.MaxValue;
                }
            }
        }

        public DateTime LastTimePlayed(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var songtocount = ctx.Songs.Single(x => x.SongId == songId);
                var concertswithsong = songtocount.SongJoinSetlists.Select(x => x.Setlist.Concert); 
                var concertsinorder = concertswithsong.OrderBy(s => s.PerformanceDate);
                var lastconcertpeformance = concertsinorder.LastOrDefault();
                if (lastconcertpeformance != null)
                {
                    return lastconcertpeformance.PerformanceDate;
                }

                else
                {
                    return DateTime.MaxValue;
                }
            }
        }
        public List<string> Get_Locations(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var songtosearch = ctx.Songs.Single(x => x.SongId == songId);
                var concertswithsong = songtosearch.SongJoinSetlists.Select(x => x.Setlist.Concert);
                var statesperformed = concertswithsong.Select(a => a.Location).ToList();
                return statesperformed;
            }
        }
    }
    
}
