using Goose.Models.Concert_Models;
using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    public class ConcertController : Controller
    {
        // GET: Concert
        public ActionResult Index()
        {
            var service = AnonymousConcertService();
            var model = service.GetConcertSetlist_List();
            return View(model);
        }

        //Get: Concert/Details/{id}
        public ActionResult Details(int id)
        {
            var service = AnonymousConcertService();
            var model = service.GetConcertById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Concert";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ConcertViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateConcertService();

            if (service.CreateConcert(model))
            {
                TempData["SaveResult"] = "Concert created";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating a concert");
            return View(model);
        }

        // Get: Concert/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateConcertService();
            var detail = service.GetConcertById(id);
            var model = new ConcertViewModel
            {
                ConcertId = detail.ConcertId,
                BandName = detail.BandName,
                DateOfPerformance = detail.DateOfPerformance,
                VenueName = detail.VenueName,
                Location = detail.Location,
                Notes = detail.Notes
            };

            return View(model);
        }

        //Post: Concert/Edit/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConcertViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ConcertId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateConcertService();

            if (service.UpdateConcert(model))
            {
                TempData["SaveResult"] = "Your concert was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your concert could not be updated.");
            return View(model);
        }

        //Get: Setlist/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateConcertService();
            var model = svc.GetConcertById(id);

            return View(model);
        }

        // POST: Concert/Delete/{id}
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CreateConcertService();

            service.DeleteConcert(id);

            TempData["SaveResult"] = "Your concert was deleted";

            return RedirectToAction("Index");
        }


        private ConcertServices CreateConcertService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConcertServices(userId);
            return service;
        }

        private ConcertServices AnonymousConcertService()
        {
            var service = new ConcertServices();
            return service;
        }
    }
}