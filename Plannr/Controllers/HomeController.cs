using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;

namespace Plannr.Controllers
{

    public class HomeController : Controller
    {
        private PlannrContext db = new PlannrContext();
        public ActionResult Index()
        {
            // Little hack, to be fixed LATER
            var init = db.Enseignants.ToList();
            ViewBag.Message = "Modifiez ce modèle pour dynamiser votre application ASP.NET MVC.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Votre page de description d’application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }
    }
}
