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
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {
                
                return PartialView("_Index");
            }
               // return View();
            
        }

        //- - - - - - - - - - - - - - - - Enseignant - - - - - - - - - - - - - - - - - -

        //Enseignant's Index
        public ActionResult IndexEnseignant()
        {
            ViewBag.count = this.enseignantRepository.Count();
           

            if (!Request.IsAjaxRequest())
            {
                return View(this.enseignantRepository.GetAll());
            }
            else
            {

                return PartialView("_IndexEnseignant", this.enseignantRepository.GetAll());
            }
            
        }

        // GET: Administration/CreateEnseignant
        public ActionResult CreateEnseignant()
        {

            if (!Request.IsAjaxRequest())
            {
                System.Diagnostics.Debug.WriteLine("test");
                return View();


            }
            else
            {
                System.Diagnostics.Debug.WriteLine("test2");
                return PartialView("_CreateEnseignant");
            }
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
               // Roles.AddUserToRole(enseignant.UserName, "Enseignant");
                //WebSecurity.CreateAccount(enseignant.UserName, enseignant.UserName);

                
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
            if (!Request.IsAjaxRequest())
            {
                return View(enseignant);
            }
            else
            {

                return PartialView("_EditEnseignant", enseignant);
            }
            
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
        
        

        // GET: /AddEnseignant/Delete

        public ActionResult DeleteEnseignant(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            else
            {
               // Membership.DeleteUser(enseignant.UserName);
               // Roles.RemoveUserFromRole(enseignant.UserName, "Enseignant");
               
                this.enseignantRepository.Delete(id);
                
                this.enseignantRepository.Save();
                return RedirectToAction("IndexEnseignant");
            }
           /* if (!Request.IsAjaxRequest())
            {
                return View(enseignant);
            }
            else
            {

                return PartialView("_DeleteEnseignant", enseignant);
            }*/

        }

        //
        // POST: /AddEnseignant/Delete

        [HttpPost, ActionName("DeleteEnseignant")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignant ens = this.enseignantRepository.Get(id);
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
            if (!Request.IsAjaxRequest())
            {
                return View(enseignant);
            }
            else
            {

                return PartialView("_DetailsEnseignant", enseignant);
            }

        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        //- - - - - - - - - - - - - - - - - Batiment - - - - - - - - - - - - - - - 
        //Batiment/Index
        public ActionResult IndexBatiment()
        {
            ViewBag.count = this.batimentRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.batimentRepository.GetAll());
            }
            else
            {
                return PartialView("_IndexBatiment", this.batimentRepository.GetAll());
            } 

        }


        // GET: /Administration/Create

        public ActionResult CreateBatiment(){
  
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateBatiment");
            } 

            }


        [HttpPost]
        public ActionResult CreateBatiment(Batiment batiment)
        {
            if (ModelState.IsValid)
            {

                this.batimentRepository.Insert(batiment);
                this.batimentRepository.Save();
                return RedirectToAction("IndexBatiment");
            }

            return View(batiment);

        }

        // GET: /Salle/Details/5

        public ActionResult DetailsBatiment(int id = 0)
        {
          
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_DetailsBatiment", batiment);
            }

        }

        //Get
        public ActionResult EditBatiment(int id = 0)
        {
   
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_EditBatiment", batiment);
            }

        }

        [HttpPost]
        public ActionResult EditBatiment(Batiment batiment)
        {
            if (ModelState.IsValid)
            {

                batimentRepository.Entry(batiment);
                batimentRepository.Save();
                return RedirectToAction("IndexBatiment");
            }
            return View(batiment);
        }

        // GET: /Batiment/Delete/

        public ActionResult DeleteBatiment(int id = 0)
        {
   
            Batiment batiment = batimentRepository.Get(id);
            if (batiment == null)
            {
                return HttpNotFound();
            } 
            else
            {
                this.batimentRepository.Delete(id);
                this.batimentRepository.Save();

                return RedirectToAction("IndexBatiment");
            }
           /* if (!Request.IsAjaxRequest())
            {
                return View(batiment);
            }
            else
            {

                return PartialView("_DeleteBatiment", batiment);
            }*/

        }

        //
         //POST: /Batiment/Delete/

        /*[HttpPost, ActionName("DeleteBatiment")]
        public ActionResult DeleteConfirmed(int id)
        {

            batimentRepository.Delete(id);
            batimentRepository.Save();
            return RedirectToAction("Index");
        }*/


        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // - - - - - - - - - - - - - - - - Salle - - - - - - - - - - - - - - - - 
        //Batiment/Index
        public ActionResult IndexSalle()
        {
            ViewBag.count = this.salleRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.salleRepository.GetList());
            }
            else
            {
                return PartialView("_IndexSalle", this.salleRepository.GetList());
            }

        }

        //Get CreateSalle
       
        public ActionResult CreateSalle()
        {
            IEnumerable<Batiment> batList = this.batimentRepository.GetAll().ToList();
            ViewBag.batsList = batList;
          
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateSalle");
            }

        }

        
        [HttpPost]
        public ActionResult CreateSalle(Salle salle)
        {
            
     
            salle.Batiment = this.batimentRepository.Get(salle.Batiment.Id);
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                this.salleRepository.Insert(salle);
                this.salleRepository.Save();
                return RedirectToAction("IndexSalle");
            }

            return View(salle);
        }

        // GET: /Salle/Edit/

        public ActionResult EditSalle(int id = 0)
        {
            Salle salle = this.salleRepository.Get(id);
            IEnumerable<Batiment> batList = this.batimentRepository.GetAll().ToList();
            ViewBag.batsList = batList;
            if (salle == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_EditSalle",salle);
            }
            
        }

        //
        // POST: /Salle/Edit

        [HttpPost]
        public ActionResult EditSalle(Salle salle)
        {
            Salle s = this.salleRepository.GetEager(salle.Id);
            s.Batiment = this.batimentRepository.Get(salle.Batiment.Id);
            s.Libelle = salle.Libelle;
            if (ModelState.IsValid)
            {

                this.salleRepository.Entry(salle);
                this.salleRepository.Save();

                return RedirectToAction("IndexSalle");
            }
            return View(salle);
        }

        // GET: /Salle/Delete

        public ActionResult DeleteSalle(int id = 0)
        {
            Salle salle = this.salleRepository.Get(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.salleRepository.Delete(id);
                this.salleRepository.Save();

                return RedirectToAction("IndexSalle");
            }
            /*if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_DeleteSalle", salle);
            }*/
            
        }

        public ActionResult DetailsSalle(int id = 0)
        {

            Salle salle = salleRepository.Get(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(salle);
            }
            else
            {

                return PartialView("_DetailsSalle", salle);
            }

        }

        //
        /* POST: /Salle/Delete

        [HttpPost, ActionName("DeleteSalle")]
        public ActionResult DeleteConfirmed(int id)
        {
           
            this.salleRepository.Delete(id);
            this.salleRepository.Save();

            return RedirectToAction("IndexSalle");
        }
         */
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //--------------------------------------------------------------
        //--------------------------Matière-------------------------------

        //Enseignant's Index
        public ActionResult IndexMatiere()
        {
            ViewBag.count = this.matiereRepository.Count();

            if (!Request.IsAjaxRequest())
            {
                return View(this.matiereRepository.GetAll());
            }
            else
            {

                return PartialView("_IndexMatiere", this.matiereRepository.GetAll());
            } 
            
        }


        // GET: Administration/CreateMatiere
        public ActionResult CreateMatiere()
        {

            IEnumerable<Ue> ueList = this.ueRepository.GetList();
            ViewBag.uesList = ueList;
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            else
            {

                return PartialView("_CreateMatiere");
            } 

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
            if (!Request.IsAjaxRequest())
            {
                return View(matiere);
            }
            else
            {

                return PartialView("_EditMatiere",matiere);
            } 

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
        public ActionResult DeleteMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            else
            {
                this.matiereRepository.Delete(id);
                this.matiereRepository.Save();

                return RedirectToAction("IndexMatiere");
            }
        }

        // GET: /Administration/Details

        public ActionResult DetailsMatiere(int id = 0)
        {
            Matiere matiere = this.matiereRepository.Get(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            if (!Request.IsAjaxRequest())
            {
                return View(matiere);
            }
            else
            {

                return PartialView("_DetailsMatiere", matiere);
            } 

        }





        protected override void Dispose(bool disposing)
        {
            this.enseignantRepository.Dispose();
            this.batimentRepository.Dispose();
            this.salleRepository.Dispose();
            this.matiereRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}