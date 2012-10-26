using System;
using System.Collections.Generic;
using System.Linq;
using Plannr.Models;

namespace Plannr.DAL
{
    public class DemandesRepository : IDemandesRepository, IDisposable
    {
        private PlannrContext context;


        public DemandesRepository(PlannrContext context)
        {
            this.context = context;
        }

        // Retourne la liste des reservations effectuées par un utilisateur qui a l'id "id"
        public IEnumerable<DemandeReservation> GetReservationsBy(int id)
        {
            return this.context.DemandesReservation.Where(x => x.Enseignement.Enseignant.UserId == id).ToList();
        }
        // REtourne la liste des reservations qu'un responsable doit vérifier
        public IEnumerable<DemandeReservation> GetReservationTo(int id)
        {
            return this.context.DemandesReservation.Where(x => x.Enseignement.Cours.Matiere.Ue.ResponsableUe.UserId == id).ToList();
        }

        public void Insert(DemandeReservation demande)
        {
            this.context.DemandesReservation.Add(demande);
        }

        public void Delete(int id)
        {
            var e = this.context.DemandesReservation.Find(id);
            this.context.DemandesReservation.Remove(e);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }


        // DIspose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}