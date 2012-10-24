using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class DemandeReservation
    {
        [Key]
        public int Id { get; set; }
        
        // Flag pour notifications pour Responsables UE
        [Required]
        public DateTime DateDemande { get; set; }
        public bool Checked { get; set; }


        // Navigators only - table de jointure
        public virtual Enseignement Enseignement {get;set;}
        public virtual CaracteristiqueSalle CaracteristiqueSalle { get; set; }
        public virtual Creneau Creneau { get; set; }

       
    }
}