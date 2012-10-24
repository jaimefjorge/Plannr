using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class CaracteristiqueSalle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="La salle a t elle un projecteur?")]
        public bool AProjecteur { get; set; }
        [Required]
        public int Capacite { get; set; }
        [Required]
        [Display(Name="La salle a t elle des prises?")]
        public bool APrises { get; set; }

        // Navigator => une caractéristique peut correspondre a plusieurs salles
        public virtual ICollection<Salle> Salles { get; set; }

    }
}