using Newtonsoft.Json;
using Plannr.DAL;
using Plannr.Filters;
using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Plannr.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ManagerController : Controller
    {
        private IDemandesRepository demandesRepository;
        private IReservationsRepository reservationsRepository;

        public ManagerController()
        {
            var db = new PlannrContext();
            this.demandesRepository = new DemandesRepository(db);
            this.reservationsRepository = new ReservationsRepository(db);
        
        }
        //
        // GET: /Manager/
        
        public ActionResult Index()
        {
            var id = (int) Membership.GetUser().ProviderUserKey;
            ViewBag.unseen = this.demandesRepository.GetUnseenDemandes(id);


            List<Reservation> resa = this.reservationsRepository.GetAll().ToList();

            List<ReservationCalendar> resaJSON = new List<ReservationCalendar>();

            resa.ForEach(x => resaJSON.Add(x.ConvertObject()));

            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ViewBag.resaJSON = JsonConvert.SerializeObject(resaJSON, Formatting.None, jsSettings);


            if (Roles.IsUserInRole(User.Identity.Name,"ResponsableUE"))
            {
                if (!Request.IsAjaxRequest())
                {
                    return View("Responsable");
                }
                else
                {
                    return PartialView("_Responsable");
                }
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
