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