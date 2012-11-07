using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plannr.Controllers
{
    public class GenerateController : Controller
    {
        private PlannrContext contxt = new PlannrContext();
        //
        // GET: /Generate/

        public ActionResult Index()
        {

            var init = this.contxt.Enseignants.Find(1);
            return View();
        }

    }
}
