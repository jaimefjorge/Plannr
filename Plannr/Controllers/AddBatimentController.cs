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
    public class AddBatimentController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IBatimentsRepository batimentRepository;


        //Constructor
        public AddBatimentController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.batimentRepository = new BatimentsRepository(context);

        }
        //
        // GET: /Salle/
        [Authorize(Roles = "ResponsableUE")]
        public ActionResult Index()
        {
            return View(db.Batiments.ToList());
        }

        //
        // GET: /Salle/Details/5

        public ActionResult Details(int id = 0)
        {
            Batiment batiment = (Batiment)db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            return View(batiment);
        }

        //
        // GET: /Salle/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Salle/Create

        [HttpPost]
        public ActionResult Create(Batiment batiment)
        {
            if (ModelState.IsValid)
            {
                db.Batiments.Add(batiment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(batiment);
        }

        //
        // GET: /Salle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Batiment batiment = (Batiment)db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            return View(batiment);
        }

        //
        // POST: /Salle/Edit/5

        [HttpPost]
        public ActionResult Edit(Batiment batiment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batiment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(batiment);
        }

        //
        // GET: /Salle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Batiment batiment = db.Batiments.Find(id);
            if (batiment == null)
            {
                return HttpNotFound();
            }
            return View(batiment);
        }

        //
        // POST: /Salle/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Batiment batiment = (Batiment)db.Batiments.Find(id);
            db.Batiments.Remove(batiment);
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