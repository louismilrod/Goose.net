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

        public IEnumerable<SongExtraDetails> GetSongOccurances(int songId)
        {
            List<SongExtraDetails> deets = new List<SongExtraDetails>();
            var songs = GetSongsBeforeAndAfter(songId);
            foreach (var item in songs)
            {
                var songextradetails = new SongExtraDetails
                {
                    SongAfter = item.SongAfter,
                    SongBefore = item.SongBefore
                };

                deets.Add(songextradetails);
            }
            return deets;

            //using (var ctx = new ApplicationDbContext())
            //{
            //    var instancesofsong_on_songjoinsetlist = ctx.SongsJoinSetlist.Where(x => x.SongId == songId);
            //    instancesofsong_on_songjoinsetlist.Select(x => new SongExtraDetails
            //    {
            //        SongAfter = 
            //    }

            //    return query.ToArray();
            //}
        }


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
                var entity = ctx.Songs.Single(s => s.SongId == id);
                double timesplayed = ctx.SongsJoinSetlist.Where(f => f.SongId == id).Count();
                double totalshows = ctx.Concerts.Count();

                return new SongDetail
                {
                    SongId = entity.SongId,
                    Title = entity.Title,
                    Artist = entity.Artist,
                    OriginalArtist = entity.OriginalArtist,
                    //Lyrics = entity.Lyrics,
                    FirstTimePlayed = FirstTimePlayed(id),
                    LastTimePlayed = LastTimePlayed(id),
                    TimesPlayed = ctx.SongsJoinSetlist.Where(f => f.SongId == id).Count(),
                    //VenuesPerformedAt = Get_Locations(id),
                    PercentageOfShows = timesplayed/totalshows,
                    
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


        public List<SongsBeforeAndAfter> GetSongsBeforeAndAfter(int songId)
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
                return allreturnedsongs.ToList();
                //to the list, selecting the first one


            }
        }

        //GETS THE FIRST PERFORMANCE OF A SONG

        public DateTime FirstTimePlayed(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var songtocount = ctx.Songs.Single(x => x.SongId == songId);
                var songjoinsetlist = songtocount.SongJoinSetlists.Select(x => x.Setlist.Concert); //selects all the concerts that a song appears on
                var concertsinorder = songjoinsetlist.OrderBy(s => s.PerformanceDate);
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

        //LAST PERFORMANCE OF SONG
        public DateTime LastTimePlayed(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var songtocount = ctx.Songs.Single(x => x.SongId == songId);
                var concertswithsong = songtocount.SongJoinSetlists.Select(x => x.Setlist.Concert); //selects all the concerts that a song appears on
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

        //var allconcert = ctx.Concerts.ToList();
        //var concertcount = ctx.Concerts.Count();
        //var sortedconcerts = allconcert.OrderBy(g => g.PerformanceDate).ToList();
        ////var instancesofsong_on_songjoinsetlist = ctx.SongsJoinSetlist.Where(x => x.SongId == songId);
        //foreach (var item in collection)
        //{

        //}



        //    List<DateTime> everyconcertevent = new List<DateTime>();

        //foreach (var item in instancesofsong_on_songjoinsetlist)
        //{
        //        var thing = item.Setlist.Concert.PerformanceDate;
        //        return thing;
        //};

        //first occurance of song in this list of sorted concerts setlists list, then select the datetime from the concert the setlist is assigned to




        public int TimesPlayed(int songId) // i spent 45 minutes writing this but had already written the linq statement up there ^^ whooops.
        {
            int timesplayed;
            using (var ctx = new ApplicationDbContext())
            {

                var allSongsJoinSetlist = ctx.SongsJoinSetlist.ToList();
                List<SongsJoinSetlist> songappearances = new List<SongsJoinSetlist>();

                foreach (var item in allSongsJoinSetlist)
                {
                    if (item.SongId == songId)
                    {
                        songappearances.Add(item);
                    }
                }

                timesplayed = songappearances.Count;
                return timesplayed;

            }
        }

        // Gap = number of occurances of a song in the db since last occurance of the song, look at the all the concerts by dateofperformance and count the performances inbetween or before

        //public int ConcertsSinceSongOccurance(int songId)
        //{
        //    int timesplayed;

        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var songtocount = ctx.Songs.Single(x => x.SongId == songId); // <-- for the real method
        //        var allconcert = ctx.Concerts.ToList();
        //        var concertcount = ctx.Concerts.Count();
        //        var sortedconcerts = allconcert.OrderBy(g => g.PerformanceDate).ToList();

        //        for (int i = 0; i < concertcount; i++)
        //        {
        //            Concert concert = sortedconcerts[i];  //creates a concert entity for each event sorted by date
        //            var sortedsetlist = sortedconcerts.Select(a => a.Setlists).ToList(); // selects the setlist in chronilogical order from the concert

        //            foreach (var item in sortedsetlist)
        //            {
        //                var songjoinsetlistsortedbydate = item.Select(x => x.SongsForSetList);

        //                foreach (var item2 in songjoinsetlistsortedbydate)
        //                {
        //                    var songoccurances = item2.Select(x => x.SongId == songId).Count();

        //                    timesplayed = songoccurances;

        //                    return songoccurances;
        //                }
        //            }
        //           // gooing to need a goto statement most likely


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
    }  //    return gap;
            //}
    
}
