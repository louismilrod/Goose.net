using Goose.Models;
using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    public class SongJoinSetlistController : Controller
    {
        // GET: SongJoinSetlistViewModelService
        public ActionResult Index()
        {
            var service = CreateSongJoinSetlistService();
            var model = service.GetSongsJoinSetlist_List();
            return View(model);
        }

        // GET: SongJoinSetlist/Details/5
        public ActionResult Details (int id)
        {
            var service = CreateSongJoinSetlistService();
            var model = service.GetSongsJoinSetlistById(id);
            return View(model);
        }

        //GET: SongJoinSetlist/Create
        public ActionResult Create()
        {
            ViewBag.Title = "New Song Joining Setlist Table";
            return View();
        }

        //POST: SongJoinSetlist/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SongsJoinSetlistViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSongJoinSetlistService();

            if (service.CreateSongJoinSetlist(model))
            {
                TempData["SaveResult"] = "Songs Joining Setlist table created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating joining table");
            return View(model);
        }

        //GET: SongJoinSetlist/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateSongJoinSetlistService();
            var detail = service.GetSongsJoinSetlistById(id);
            var model = new SongsJoinSetlistViewModel
            {
                SongsJoinSetlistId = detail.SongsJoinSetlistId,
                SetlistId = detail.SetlistId,
                SongId = detail.SongId,
                PositionInSet = detail.PositionInSet
            };

            return View(model);
        }

        //Post: SongJoinSetlist/Edit/5
        [HttpPost, ValidateAntiForgeryToken]

        public ActionResult Edit(int id, SongsJoinSetlistViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            if(model.SetlistId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSongJoinSetlistService();

            if (service.UpdateSongJoinSetlist(model))
            {
                TempData["SaveResult"] = "Songs Joining Setlist table created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your joining table was not updated");
            return View(model);
        }

        //GET: SongJoinSetlist/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete (int id)
        {
            var svc = CreateSongJoinSetlistService();
            var model = svc.GetSongsJoinSetlistById(id);

            return View(model);
        }

        //POST: Setlist/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSongJoinSetlistService();
            service.DeleteSongJoiningSetlist(id);

            TempData["Save Result"] = "Your joining table is deleted";

            return RedirectToAction("Index");
        }

        private SongJoinSetlistService CreateSongJoinSetlistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongJoinSetlistService(userId);
            return service;
        }
    }
}