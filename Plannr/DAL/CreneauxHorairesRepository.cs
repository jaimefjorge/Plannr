using Plannr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.DAL
{
    public class CreneauxHorairesRepository : ICreneauxHorairesRepository
    {

        private PlannrContext context;

        public CreneauxHorairesRepository(PlannrContext context)
        {
            this.context = context;
        }

        public IEnumerable<CreneauHoraire> getCreneauxHoraires()
        {
            return this.context.CreneauxHoraires.ToList();
        }

        public CreneauHoraire Find(int id)
        {
            return this.context.CreneauxHoraires.Find(id);
        }

        // Renvoie une liste de creneaux horaires disponibles pour une date donnée. Evite donc les créneaux déjà utilisés.
        public IEnumerable<CreneauHoraire> getCreneauxHorairesForDate(DateTime date)
        {
            return (from c in this.context.CreneauxHoraires.ToList()
                    where !(from resa in this.context.Reservations where resa.Date == date select resa.Creneau.Id).Contains(c.Id)
                    select c);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}