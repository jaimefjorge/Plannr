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
    public class ReservationsController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IReservationsRepository repository;
       
          // Constructor
        public ReservationsController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.repository = new ReservationsRepository(context);

        }

        // Give it as a parameter aswel for unit testing
        public ReservationsController(IReservationsRepository repo)
        {
            this.repository = repo;

        }


        // GET: /Reservations/

        public ActionResult Index()
        {
            return View(this.repository.GetAll());
        }


        //
        // GET: /Reservations/Create/id_demande

        public ActionResult Create(int id)
        {
            return View();
        }

        //
        // POST: /Reservations/Create

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        //
        // GET: /Reservations/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        //
        // POST: /Reservations/Edit/5

        [HttpPost]
        public ActionResult Edit(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        //
        // POST: /Reservations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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