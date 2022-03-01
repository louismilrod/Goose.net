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
        public ActionResult Index(string sortorder)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortorder) ? "date" : "";

            var service = AnonymousConcertService();
            var model = service.GetConcert_List();
            var concerts = from c in model select c;
            switch (sortorder)
            {
                case "date":
                    concerts = model.OrderBy(a => a.DateOfPerformance);
                    break;
                default:
                    break;
            }
            return View(concerts.ToList());
        }

        //Get: Concert/Details/{id}
        public ActionResult Details(int id)
        {
            var service = AnonymousConcertService();
            var model = service.GetConcertById(id);
            return View(model);
        }

        //Get: Concert Attendance
        [Authorize]
        public ActionResult Attended(int id)
        {
            var service = CreateConcertService();
            if (service.I_Went_To_That(id) == false)
            {
                TempData["AlreadySeen"] = "You already went to that concert";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Added"] = "Concert Added";
                return RedirectToAction("Index");
            }
            

        }

        public ActionResult Unattend(int id)
        {
            var service = CreateConcertService();            

            if (service.Wait_Did_I_Go_To_That(id) == false)
            {
                TempData["SaveResult"] = "I knew you didn't go";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Title = "New Concert";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var svc = CreateConcertService();
            var model = svc.GetConcertById(id);

            return View(model);
        }

        // POST: Concert/Delete/{id}
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
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