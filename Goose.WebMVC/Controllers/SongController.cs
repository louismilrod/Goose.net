using Goose.Models.Song_Models;
using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    public class SongController : Controller
    {
        // GET: Song
        public ActionResult Index(string sortorder)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortorder) ? "title_desc" : "";
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortorder) ? "id_desc" : "";
            ViewBag.CountSortParm = String.IsNullOrEmpty(sortorder) ? "count" : "";
            ViewBag.AvgSortParm = String.IsNullOrEmpty(sortorder) ? "avg" : "";
            ViewBag.FTPSortParm = String.IsNullOrEmpty(sortorder) ? "ftp" : "";
            ViewBag.LTPSortParm = String.IsNullOrEmpty(sortorder) ? "ltp" : "";
            ViewBag.OrigSortParm = String.IsNullOrEmpty(sortorder) ? "orig" : "";
            
            var service = AnonymousServiceView();
            var model = service.GetSongLists();
            var songs = from s in model select s;
            switch (sortorder)
            {
                case "title_desc":
                   songs = model.OrderByDescending(a => a.Title);
                    break;
                case "id_desc":
                    songs = model.OrderBy(a => a.SongId);
                    break;
                case "count":
                    songs = model.OrderByDescending(a =>a.TimesPlayed);
                    break;
                case "avg":
                    songs = model.OrderByDescending(a => a.AvgTimesPlayed);
                    break;
                case "ftp":
                    songs = model.OrderByDescending(a => a.FirstTimePlayed);
                    break;
                case "ltp":
                    songs = model.OrderByDescending(a => a.LastTimePlayed);
                    break;
                case "orig":
                    songs = model.OrderBy(a => a.OriginalArtist);
                    break;
                default:
                    songs = model.OrderBy(b=>b.Title);
                    break;
            }
            return View(songs.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Title = "New Song";
            return View();
        }

        //POST: Song
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(SongCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSongService();

            if (service.CreateSong(model))
            {
                TempData["SaveResult"] = "Song created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating a song");
            return View(model);
        }        

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var service = CreateSongService();
            var detail = service.GetSongById(id);
            var model =
                new SongEdit
                {
                    SongId = detail.SongId,
                    Title = detail.Title,
                    Artist = detail.Artist,
                    OriginalArtist = detail.OriginalArtist,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, SongEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SongId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSongService();

            if (service.UpdateSong(model))
            {
                TempData["SaveResult"] = "Your song was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your song could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSongService();

            service.DeleteSong(id);

            TempData["SaveResult"] = "Your song was deleted";

            return RedirectToAction("Index");
        }
        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            return service;
        }

        private SongService AnonymousServiceView()
        {
            var service = new SongService();
                return service;
        }
    }
}