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
    public class AddSalleController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private ISallesRepository salleRepository;


        //Constructor
        public AddSalleController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.salleRepository = new SallesRepository(context);

        }
        //
        // GET: /Salle/
        [Authorize(Roles = "ResponsableUE")]
        public ActionResult Index()
        {
            return View(db.Salles.ToList());
        }

        //
        // GET: /Salle/Details/5

        public ActionResult Details(int id = 0)
        {
            Salle salle = (Salle)db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        //
        // GET: /Salle/Create

        public ActionResult Create()
        {
            ViewData["listeBatiment"] = db.Batiments.ToList();

            return View();
        }

        //
        // POST: /Salle/Create

        [HttpPost]
        public ActionResult Create(Salle salle)
        {
            if (ModelState.IsValid)
            {
                db.Salles.Add(salle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salle);
        }

        //
        // GET: /Salle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Salle salle = (Salle)db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        //
        // POST: /Salle/Edit/5

        [HttpPost]
        public ActionResult Edit(Salle salle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salle);
        }

        //
        // GET: /Salle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Salle salle = db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        //
        // POST: /Salle/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Salle salle = (Salle)db.Salles.Find(id);
            db.Salles.Remove(salle);
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