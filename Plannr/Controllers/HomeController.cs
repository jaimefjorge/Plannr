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

            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manager");
            }

            return View();
        }

      
    }
}
