using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using System.Web.Security;
using Plannr.DAL;
using Newtonsoft.Json;
using Plannr.Filters;
using WebMatrix.WebData;
using Microsoft.Web.WebPages.OAuth;

namespace Plannr.Controllers
{
    [Authorize(Roles = "ResponsableUE")]
    [InitializeSimpleMembership]
    public class AdministrationController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IEnseignantsRepository enseignantRepository;
        private ISallesRepository salleRepository;
        private IBatimentsRepository batimentRepository;
        private IMatieresRepository matiereRepository;
        private IUeRepository ueRepository;

        //
        // GET: /Administration/

        public AdministrationController() {
            var context = new PlannrContext();
            enseignantRepository = new EnseignantsRepository(context);
            salleRepository = new SallesRepository(context);
            batimentRepository = new BatimentsRepository(context);
            matiereRepository = new MatieresRepository(context);
            ueRepository = new UeRepository(context);
        }

        public AdministrationController(IEnseignantsRepository repo, IBatimentsRepository batrepo, ISallesRepository salrepo, IMatieresRepository matrepo, IUeRepository uerepo)
        {
            this.enseignantRepository = repo;
            this.batimentRepository = batrepo;
            this.salleRepository = salrepo;
            this.matiereRepository = matrepo;
            this.ueRepository = uerepo;
        }

        //Administration's Index
        public ActionResult Index()
        {
           
                return View();
            
        }

        //- - - - - - - - - - - - - - - - Enseignant - - - - - - - - - - - - - - - - - -

        //Enseignant's Index
        public ActionResult IndexEnseignant()
        {
            return View(this.enseignantRepository.GetAll());
        }

        // GET: Administration/CreateEnseignant
        public ActionResult CreateEnseignant()
        {

            return View();
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateEnseignant(Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {

                this.enseignantRepository.Insert(enseignant);
                this.enseignantRepository.Save();
                //WebSecurity.CreateAccount(enseignant.UserName, enseignant.UserName);
                //Roles.AddUserToRole(enseignant.UserName, "Enseignant");
                
                return RedirectToAction("IndexEnseignant");
            }

            return View(enseignant);

        }

        // GET: /AddEnseignant/Edit

        public ActionResult EditEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        //
        // POST: /AddEnseignant/Edit

        [HttpPost]
        public ActionResult EditEnseignant(Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                this.enseignantRepository.Edit(enseignant);

                this.enseignantRepository.Save();
                return RedirectToAction("IndexEnseignant");
            }
            return View(enseignant);
        }
        
        public ActionResult CreateSalle() {
            ViewBag.batiments = this.batimentRepository.GetAll().ToList();
            return View();
        }

        // GET: /AddEnseignant/Delete

        public ActionResult DeleteEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        //
        // POST: /AddEnseignant/Delete

        [HttpPost, ActionName("DeleteEnseignant")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignant ens = this.enseignantRepository.Get(id);
            //System.Web.Security.Membership.DeleteUser(ens.UserName);
            this.enseignantRepository.Delete(id);
            
            this.enseignantRepository.Save();
            return RedirectToAction("IndexEnseignant");
        }

        // GET: /Administration/Details

        public ActionResult DetailsEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        //- - - - - - - - - - - - - - - - - Batiment - - - - - - - - - - - - - - - 

        // GET: /Administration/Create

        public ActionResult CreateBatiment(){
             //ViewBag.batiments = this.batimentRepository.GetAll();
          return View();
            }


        [HttpPost]
        public ActionResult CreateBatiment(Batiment batiment)
        {
            if (ModelState.IsValid)
            {

                this.batimentRepository.Insert(batiment);
                this.batimentRepository.Save();
                return RedirectToAction("Index");
            }

            return View(batiment);

        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // - - - - - - - - - - - - - - - - Salle - - - - - - - - - - - - - - - - 

        [HttpPost]
        public ActionResult CreateSalle(Salle salle)
        {
            if (ModelState.IsValid)
            {

                this.salleRepository.Insert(salle);
                this.salleRepository.Save();
                return RedirectToAction("Index");
            }

            return View(salle);

        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        // POST: /Administration/Edit/5

        [HttpPost]
        public ActionResult Edit(Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enseignant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enseignant);
        }


        //--------------------------------------------------------------
        //--------------------------Matière-------------------------------

        //Enseignant's Index
        public ActionResult IndexMatiere()
        {
            return View(this.matiereRepository.GetAll());
        }


        // GET: Administration/CreateMatiere
        public ActionResult CreateMatiere()
        {

            IEnumerable<Ue> ueList = this.ueRepository.GetList();
            ViewBag.uesList = ueList;
            return View();
        }



        
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult CreateMatiere(Matiere matiere)
        {

            matiere.Ue = this.ueRepository.Get(matiere.Ue.Id);
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                this.matiereRepository.Insert(matiere);
                this.matiereRepository.Save();
                return RedirectToAction("IndexMatiere");
            }

            return View(matiere);

        }

        // GET: /AddMatiere/Edit

        public ActionResult EditMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            IEnumerable<Ue> ueList = this.ueRepository.GetList();
            ViewBag.uesList = ueList;
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }

        //
        // POST: /AddMatiere/Edit

        [HttpPost]
        public ActionResult EditMatiere(Matiere matiere)
        {
            Matiere m = this.matiereRepository.GetEager(matiere.Id);
            m.Ue = this.ueRepository.Get(matiere.Ue.Id);
            m.Libelle = matiere.Libelle;
          
            if (ModelState.IsValid)
            {
                this.matiereRepository.Edit(m);       
                this.matiereRepository.Save();
                return RedirectToAction("IndexMatiere");
            }

            return View(m);
        }








        // GET: /AddMatiere/Delete
        public ActionResult DeleteMatiere(int id)
        {
            this.matiereRepository.Delete(id);
            this.matiereRepository.Save();
            return RedirectToAction("IndexMatiere");
        }

        // GET: /Administration/Details

        public ActionResult DetailsMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            return View(matiere);
        }





        protected override void Dispose(bool disposing)
        {
            this.enseignantRepository.Dispose();
            this.batimentRepository.Dispose();
            this.salleRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}