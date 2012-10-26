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
using Plannr.DAL;

namespace Plannr.Controllers
{
    public class BookController : Controller
    {   
        // Repository as private member, generic interface type
        private IDemandesRepository repository;
        private IEnseignementsRepository enseignementsRepository;

        // Constructor
        public BookController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.repository = new DemandesRepository(context);
            this.enseignementsRepository = new EnseignementsRepository(context);
        }

        // Give it as a parameter aswel
        public BookController(IDemandesRepository repo, IEnseignementsRepository ensRepo)
        {
            this.repository = repo;
            this.enseignementsRepository = ensRepo;
        }


        //
        // GET: /Book/
       [Authorize(Roles="Enseignant")]
        public ActionResult Index() {

            var id = (int) Membership.GetUser().ProviderUserKey; 
    
            return View(this.repository.GetReservationsBy(id));
        }


        //
        // GET: /Book/Create
        [Authorize(Roles = "Enseignant")]
        public ActionResult Create()
        {
            // Get Session from Current Logged User
            var id = (int)Membership.GetUser().ProviderUserKey;
            // On va chercher les enseignements qui sont attribués à l'enseignant pour les lister dans la DropList en sélectionner un, on le passe à la vue pour génrer la ListBox
            ViewBag.listEnseignements = this.enseignementsRepository.GetEnseignementsForTeacher(id);
                                    
            return View();
        }

        //
        // POST: /Book/Create
        [Authorize(Roles = "Enseignant")]
        [HttpPost]
        public ActionResult Create(DemandeReservation demandereservation) {
            
            demandereservation.DateDemande = DateTime.Now;
            demandereservation.Checked = false;
            // Mapping
            demandereservation.Enseignement = this.enseignementsRepository.Get(demandereservation.Enseignement.Id);

            if (ModelState.IsValid)
            {
                repository.Insert(demandereservation);
                repository.Save();
              
                return RedirectToAction("Index");
            }


            // Since it's returing view, we need the listEnseignements in the ViewBag aswell
            var id = (int)Membership.GetUser().ProviderUserKey;
            ViewBag.listEnseignements = this.enseignementsRepository.GetEnseignementsForTeacher(id);

 

            return View(demandereservation);
        }

  /*
        //
        // GET: /Book/Delete/5
        [Authorize(Roles = "Enseignant")]
        public ActionResult Delete(int id = 0)
        {
            DemandeReservation demandereservation = db.DemandesReservation.Find(id);
            if (demandereservation == null)
            {
                return HttpNotFound();
            }
            return View(demandereservation);
        }*/

        //
        // POST: /Book/Delete/5
        [Authorize(Roles = "Enseignant")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.repository.Delete(id);
            this.repository.Save();
            return RedirectToAction("Index");
        }

    }
}