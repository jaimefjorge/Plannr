using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Plannr.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        [Authorize]
        public ActionResult Index()
        {
          

            if (Roles.IsUserInRole(User.Identity.Name,"ResponsableUE"))
            {
                return View("Responsable");
            }
            else if (Roles.IsUserInRole(User.Identity.Name,"Enseignant"))
            {
                return View("Enseignant");
            }
            else
            {
                return View();
            }

        }
       

    }
}
