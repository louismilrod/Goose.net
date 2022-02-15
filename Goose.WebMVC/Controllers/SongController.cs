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