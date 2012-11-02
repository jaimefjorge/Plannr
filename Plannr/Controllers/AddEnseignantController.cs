using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using Plannr.DAL;

namespace Plannr.Controllers
{
    public class AddEnseignantController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IEnseignantsRepository enseignantRepository;


        //onstructor
        public AddEnseignantController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.enseignantRepository = new EnseignantsRepository(context);

        }


        // GET: /AddEnseignant/

       [Authorize(Roles = "ResponsableUE")]

        public ActionResult Index()
        {
            return View(db.Enseignants.ToList());
        }

        //
        // GET: /AddEnseignant/Details/5

        public ActionResult Details(int id = 0)
        {
            Enseignant enseignant = (Enseignant)db.Personnes.Find(id);
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
                db.Personnes.Add(enseignant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enseignant);
        }

        //
        // GET: /AddEnseignant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Enseignant enseignant = (Enseignant)db.Personnes.Find(id);
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
                db.Entry(enseignant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enseignant);
        }

        //
        // GET: /AddEnseignant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Enseignant enseignant = (Enseignant)db.Personnes.Find(id);
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
            Enseignant enseignant = (Enseignant)db.Personnes.Find(id);
            db.Personnes.Remove(enseignant);
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