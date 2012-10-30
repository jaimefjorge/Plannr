using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Plannr.Models;
using Plannr.DAL;

namespace Plannr.API
{
    public class BookController : ApiController
    {
        private IDemandesRepository repository;
        private IEnseignementsRepository enseignementsRepository;
        private PlannrContext db = new PlannrContext();
       

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


        public IEnumerable<DemandeReservation> GetDemandeReservations()
        {

            return db.DemandesReservation.AsEnumerable();
        }

        public DemandeReservation GetDemande(int id)
        {
            return this.repository.Find(id);
        }

        public HttpResponseMessage Delete(int id)
        {
            var demande = this.repository.Find(id);
            this.repository.Delete(demande);
            this.repository.Save();
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }

        
}