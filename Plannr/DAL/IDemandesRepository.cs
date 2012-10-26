using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
namespace Plannr.DAL
{
    public interface IDemandesRepository : IDisposable
    {
        // Get reservations faites par une ID
        IEnumerable<DemandeReservation> GetReservationsBy(int id);
        // Get reservations qu'un user ID doit valider (un resp ue)
        IEnumerable<DemandeReservation> GetReservationTo(int id);
        // 
        void Insert(DemandeReservation demande);
        void Delete(int id);
        void Save();

    }
}