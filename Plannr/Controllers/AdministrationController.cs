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

namespace Plannr.Controllers
{
    public class AdministrationController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IEnseignantsRepository enseignantRepository;
        private ISallesRepository salleRepository;
        private IBatimentsRepository batimentRepository;

        //
        // GET: /Administration/

        public AdministrationController() {
            var context = new PlannrContext();
            enseignantRepository = new EnseignantsRepository(context);
            salleRepository = new SallesRepository(context);
            batimentRepository = new BatimentsRepository(context);
        }

        public ActionResult Index()
        {
           /* ViewBag.batiments = this.batimentRepository.GetAll();
            ViewBag.enseignants = this.enseignantRepository.GetAll();
            ViewBag.batiments = this.salleRepository.GetList();*/
            
                return View();
            
           
        }

        public ActionResult CreateSalle() {
            ViewBag.batiments = this.batimentRepository.GetAll();
            return View();
        }

        //
        // GET: /Administration/Details/5

     
        //
        // GET: /Administration/Create

        public ActionResult CreateBatiment(){
             //ViewBag.batiments = this.batimentRepository.GetAll();
          return View();
            }

 
      

        public ActionResult CreateEnseignant()
        {
     //ViewBag.batiments = this.batimentRepository.GetAll();
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
                return RedirectToAction("Index");
            }

            return View(enseignant);
      
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


        //
        // GET: /Administration/Edit/5

    

        //
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

        //
        // GET: /Administration/Delete/5

       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}