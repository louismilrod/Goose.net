using Goose.Models.Setlist_Models;
using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    public class SetlistController : Controller
    {
        // GET: Setlist
        public ActionResult Index()
        {
            var service = AnonymousSetListService();
            var model = service.GetSetlist_List();
            return View(model);
        }

        // GET: Setlist/Details/5
        public ActionResult Details(int id)
        {
            var service = AnonymousSetListService();
            var model = service.GetSetlistById(id);
            return View(model);
        }

        // GET: Setlist/Create
        public ActionResult Create()
        {
            ViewBag.Title = "New Setlist";
            return View();
        }

        // POST: Setlist/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SetlistViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var service = CreateSetListService();

            if (service.CreateSetlist(model))
            {
                TempData["SaveResult"] = "Song created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating a song");
            return View(model);
        }

        // GET: Setlist/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateSetListService();
            var detail = service.GetSetlistById(id);
            var model = new SetlistViewModel
            {
                SetlistId = detail.SetlistId,
                SetNumber = detail.SetNumber,
                SongsForSetlist = detail.SongsForSetlist,
                DateOfPerformance = detail.DateOfPerformance
            };

            return View(model);
        }

        // POST: Setlist/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SetlistViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SetlistId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSetListService();

            if (service.UpdateSetlist(model))
            {
                TempData["SaveResult"] = "Your setlist was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your song could not be updated.");
            return View(model);
        }

        // GET: Setlist/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSetListService();
            var model = svc.GetSetlistById(id);

            return View(model);
        }

        // POST: Setlist/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSetListService();

            service.DeleteSetlist(id);

            TempData["SaveResult"] = "Your setlist was deleted";

            return RedirectToAction("Index");
        }

        private SetlistService CreateSetListService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SetlistService(userId);
            return service;
        }

        private SetlistService AnonymousSetListService()
        {
            var service = new SetlistService();
            return service;
        }
    }
}
