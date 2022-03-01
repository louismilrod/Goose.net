using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            var service = ConcertsAttendedServices();
            var model = service.GetConcertsAttended();
            return View(model);
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

        private ConcertsAttendedServices ConcertsAttendedServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConcertsAttendedServices(userId);
            return service;
        }

        private ConcertServices CreateConcertService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConcertServices(userId);
            return service;
        }
    }
}