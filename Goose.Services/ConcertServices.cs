﻿using Goose.Data;
using Goose.Data.Data;
using Goose.Models;
using Goose.Models.Concert_Models;
using Goose.Models.Setlist_Models;
using Goose.Models.Song_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class ConcertServices
    {
        private Guid _userId { get; set; }

        public ConcertServices()
        {

        }

        public ConcertServices(Guid userId)
        {
            _userId = userId;
        }

        //public async Task<List<ConcertListItem>> GetConcert_List()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var attendancetable = ctx.ConcertsAttended.Where(u => u.UserId == _userId);
        //        //empty list, foreach through concerts, and add to the list
        //        var list = new List<ConcertListItem>();
        //        foreach (var item in ctx.Concerts)
        //        {
        //            list.Add(new ConcertListItem
        //            {
        //                ConcertId = item.ConcertId,
        //                BandName = item.BandName,
        //                DateOfPerformance = item.PerformanceDate,
        //                VenueName = item.VenueName,
        //                Location = item.Location,
        //                Notes = item.Notes,
        //                InAttendance = attendancetable.Where(c => c.ConcertId == item.ConcertId).Count() > 0,
        //                Setlists = item.Setlists.add!!!(a => new SetlistDataForConcertDetailView
        //                {
        //                    SetlistId = a.SetlistId,
        //                    SetNumber = a.SetNumber,
        //                    SongsForSetlist = a.SongsForSetList.Select(b => new SongDetail
        //                    {
        //                        Title = b.Song.Title,
        //                        SongId = b.Song.SongId,
        //                    }).ToList(),
        //                }).ToList()

        //            });
        //        }
        //        return list;
        //    }
        //}

        public IEnumerable<ConcertListItem> GetConcert_List()
        {
            //bool attendance = Was_I_There();

            using (var ctx = new ApplicationDbContext())            {

                var attendancetable = ctx.ConcertsAttended.Where(u => u.UserId == _userId);
                //in attendance logic
                var query = ctx.Concerts.Select(s => new ConcertListItem
                {
                    ConcertId = s.ConcertId,
                    BandName = s.BandName,
                    DateOfPerformance = s.PerformanceDate,
                    VenueName = s.VenueName,
                    Location = s.Location,
                    Notes = s.Notes,
                    InAttendance = attendancetable.Where(c => c.ConcertId == s.ConcertId).Count() > 0,
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
                });

                return query.ToArray();
            }
        }


        public bool Was_I_There()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var attendancetable = ctx.ConcertsAttended.Where(u => u.UserId == _userId);
                var query = ctx.Concerts.Select(u => u.ConcertId).ToList();

                List<ConcertListItem> concertId = new List<ConcertListItem>();
                foreach (var item in attendancetable)
                {

                    if (query.Contains(item.ConcertId))
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }

                }

                return false;

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

        public ConcertDetails GetConcertById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Concerts.Single(c => c.ConcertId == id);

                return new ConcertDetails
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

        //public bool I_Went_To_That(I_Went_To_That_Model model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var concertsattended = new ConcertsAttended()
        //        {
        //            UserId = _userId,
        //            ConcertId = model.ConcertId
        //        };
        //        model.InAttendance = true;

        //        ctx.ConcertsAttended.Add(concertsattended);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        public bool I_Went_To_That(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var concertsattended = new ConcertsAttended()
                {
                    UserId = _userId,
                    ConcertId = concertId,
                };
                var inattendance = ctx.ConcertsAttended.Where(x => x.ConcertId == concertId).Where(x => x.UserId == _userId);

                if (inattendance.Any())//++user id
                {
                    return false;
                }

                ctx.ConcertsAttended.Add(concertsattended);
                return ctx.SaveChanges() == 1;
            }
        }

        public bool Wait_Did_I_Go_To_That(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.ConcertsAttended.SingleOrDefault(x=>x.ConcertId==concertId && x.UserId == _userId);
                
                if (entity == null)
                {
                    return false;
                }

                ctx.ConcertsAttended.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }


        //public bool Wait_Did_I_Go_To_That(Wait_Did_I_Go_To_That model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity = ctx.ConcertsAttended.Single(x => x.ConcertsAttendedId == model.ConcertsAttendedId);
        //        model.InAttendance = false;
        //        ctx.ConcertsAttended.Remove(entity);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

       
    }
}
