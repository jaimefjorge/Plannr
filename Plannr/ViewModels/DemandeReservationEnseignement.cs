using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plannr.Models;
using System.ComponentModel.DataAnnotations;

namespace Plannr.ViewModels
{
    public class DemandeReservationEnseignement
    {
        [Required]
        public DemandeReservation DemandeReservation { get; set; }
        [Required]
        public List<Enseignement> Enseignements { get; set; }
    }
}