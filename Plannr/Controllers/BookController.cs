using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Plannr.Controllers
{
    public class BookController : Controller
    {
        private PlannrContext db = new PlannrContext();

        //
        // GET: /Book/
       [Authorize(Roles="Enseignant")]
        public ActionResult Index() {

            
            var id = (int) Membership.GetUser().ProviderUserKey; 
            //var id = 1;
            return View(db.DemandesReservation.Where(x => x.Enseignement.Enseignant.UserId == id).ToList());
        }


        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            var id = (int)Membership.GetUser().ProviderUserKey;
            // On va chercher les enseignements qui sont attribués à l'enseignant pour les lister dans la DropList en sélectionner un, on le passe à la vue pour génrer la ListBox
            ViewBag.listEnseignements = db.Enseignements.Where(x => x.Enseignant.UserId == id).ToList();
                                    

            return View();
        }

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(DemandeReservation demandereservation) {
            
            demandereservation.DateDemande = DateTime.Now;
            demandereservation.Checked = false;
            // Mapping
            demandereservation.Enseignement = db.Enseignements.Find(demandereservation.Enseignement.Id);

            if (ModelState.IsValid)
            {
                db.DemandesReservation.Add(demandereservation);
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }

            var id = (int)Membership.GetUser().ProviderUserKey;
            ViewBag.listEnseignements = db.Enseignements.Where(x => x.Enseignant.UserId == id).ToList();

 

            return View(demandereservation);
        }

  
        //
        // GET: /Book/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            db.DemandesReservation.Remove(demandereservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}