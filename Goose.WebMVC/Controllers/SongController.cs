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
        public ActionResult Index()
        {
            var service = AnonymousServiceView();
            var model = service.GetSongLists();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Song";
            return View();
        }

        //POST: Song
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Details(int id)
        {
            var svc = AnonymousServiceView();
            var model = svc.GetSongById(id);

            return View(model);
        }

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
        public ActionResult Delete(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
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