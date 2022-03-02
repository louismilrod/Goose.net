using Goose.Models.Setlist_Models;
using Goose.Models.Setlist_Modles;
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
    public class SetlistController : Controller
    {
        // GET: Setlist
        public ActionResult Index()
        {
            var service = AnonymousSetListService();
            var model = service.GetSetlist_List();
            return View(model);
        }        

        // GET: Setlist/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Title = "New Setlist";
            var service = CreateSetListService();
            SelectList selectListItems = service.SelectListPopulator();
            SetlistCreate model = new SetlistCreate()
            {
                SelectListSetlist = selectListItems
            };
            return View(model);
        }

        // POST: Setlist/Create
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(SetlistCreate model)
        {
            if(!ModelState.IsValid) return View(model);

            var service = CreateSetListService();

            if (service.CreateSetlist(model))
            {
                TempData["SaveResult"] = "Setlist created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating a setlist");
            return View(model);
        }

        // GET: Setlist/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var service = CreateSetListService();
            var detail = service.GetSetlistById(id);
            SelectList selectListItems = service.SelectListPopulator();
            var model = new SetlistViewModel
            {
                ConcertId = detail.ConcertId,
                SetlistId = detail.SetlistId,
                SetNumber = detail.SetNumber,
                SelectListSetlist = selectListItems,
            };

            return View(model);
        }

        // POST: Setlist/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, SetlistViewModel model)
        {
            var service = CreateSetListService();
            SelectList selectListItems = service.SelectListPopulator();
            model.SelectListSetlist = selectListItems;

            if (!ModelState.IsValid) return View(model);

            if (model.SetlistId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }


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
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSetListService();
            var model = svc.GetSetlistById(id);

            return View(model);
        }

        // POST: Setlist/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
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
