using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plannr.Models;
using Plannr.DAL;
using System.Web.Security;
using Plannr.Filters;

namespace Plannr.Controllers
{
    [Authorize(Roles="ResponsableUE")]
    [InitializeSimpleMembership]

    public class ReservationsController : Controller
    {
        private PlannrContext db = new PlannrContext();
        private IReservationsRepository repository;
        private IDemandesRepository demandesRepository;
        private ISallesRepository sallesRepository;
        private ICreneauxHorairesRepository creneauxRepository;
       
          // Constructor
        public ReservationsController()
        {
            // Share same context for both repo
            var context = new PlannrContext();
            this.repository = new ReservationsRepository(context);
            this.demandesRepository = new DemandesRepository(context);
            this.sallesRepository = new SallesRepository(context);
            this.creneauxRepository = new CreneauxHorairesRepository(context);
        }

        // Give it as a parameter aswel for unit testing
        public ReservationsController(IReservationsRepository repo, IDemandesRepository demandesRepo, ISallesRepository sallesRepo, ICreneauxHorairesRepository creneauxRepo)
        {
            this.repository = repo;
            this.demandesRepository = demandesRepo;
            this.sallesRepository = sallesRepo;
            this.creneauxRepository = creneauxRepo;

        }


        // GET: /Reservations/

        public ActionResult Index() 
        {
            var id = (int)Membership.GetUser().ProviderUserKey;
            return View(this.demandesRepository.GetReservationTo(id));
        }


        //
        // GET: /Reservations/Create/id_demande

        public ActionResult Create(int id)
        {
            var demandeAssociee = this.demandesRepository.Find(id);
            List<Salle> salles = (List<Salle>) this.sallesRepository.GetSallesCriteres(demandeAssociee.CapaciteNecessaire, demandeAssociee.BesoinProjecteur, demandeAssociee.DateVoulue);
            List<CreneauHoraire> creneaux = this.creneauxRepository.getCreneauxHorairesForDate(demandeAssociee.DateVoulue).ToList();


            creneaux.ForEach(x => System.Diagnostics.Debug.WriteLine(x.HeureConcat));

            ViewBag.demandeAssociee = demandeAssociee;
            ViewBag.salles = salles;
            ViewBag.creneaux = creneaux;



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