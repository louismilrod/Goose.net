using Goose.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goose.WebMVC.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            var service = ConcertsAttendedServices();
            var model = service.GetConcertsAttended();
            return View(model);
        }

        private ConcertsAttendedServices ConcertsAttendedServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ConcertsAttendedServices(userId);
            return service;
        }
    }
}