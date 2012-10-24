using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        // Date à laquelle la réservation est validée
        [Required]
        public DateTime DateValidation { get; set; }


        public virtual Creneau Creneau { get; set; }
        public virtual Enseignement Enseignement { get; set; }
        public virtual Salle Salle { get; set; }
        // Le responsable qui valide la resa
        public virtual ResponsableUE ResponsableUe { get; set; }

    }
}