using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using Plannr.DAL;
using Plannr.Filters;
using WebMatrix.WebData;
using System.Web.Security;

namespace Plannr.Controllers
{
           [Authorize(Roles = "ResponsableUE")]
    [InitializeSimpleMembership]
    public class AddEnseignantController : Controller
    {
        
        private IEnseignantsRepository enseignantRepository;


        //onstructor
        public AddEnseignantController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.enseignantRepository = new EnseignantsRepository(context);

        }

        public AddEnseignantController(IEnseignantsRepository repo)
        {
            this.enseignantRepository = repo;
        }

        
        


        // GET: /AddEnseignant/



        public ActionResult Index()
        {
            return View(this.enseignantRepository.GetAll());
        }

        //
        // GET: /AddEnseignant/Details/5

        public ActionResult Details(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        //
        // GET: /AddEnseignant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AddEnseignant/Create

        [HttpPost]
        public ActionResult Create(Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                
                this.enseignantRepository.Insert(enseignant);
                this.enseignantRepository.Save();
                WebSecurity.CreateAccount(enseignant.UserName, enseignant.UserName);
                Roles.AddUserToRole(enseignant.UserName, "Enseignant");
                return RedirectToAction("Index");
            }

            return View(enseignant);
        }

        //
        // GET: /AddEnseignant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        //
        // POST: /AddEnseignant/Edit/5

        [HttpPost]
        public ActionResult Edit(Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                this.enseignantRepository.Edit(enseignant);

                this.enseignantRepository.Save();
                return RedirectToAction("Index");
            }
            return View(enseignant);
        }

        //
        // GET: /AddEnseignant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Enseignant enseignant = this.enseignantRepository.Get(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        //
        // POST: /AddEnseignant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
        
            this.enseignantRepository.Delete(id);
            this.enseignantRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.enseignantRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}