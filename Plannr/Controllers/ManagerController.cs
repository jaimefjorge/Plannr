using Plannr.DAL;
using Plannr.Models;
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
        private IDemandesRepository demandesRepository;

        public ManagerController()
        {
            var db = new PlannrContext();
            this.demandesRepository = new DemandesRepository(db);
        }
        //
        // GET: /Manager/
        [Authorize]
        public ActionResult Index()
        {
            var id = (int) Membership.GetUser().ProviderUserKey;
            ViewBag.unseen = this.demandesRepository.GetUnseenDemandes(id).Count();

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
