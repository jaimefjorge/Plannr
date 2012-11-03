using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Salle
    {
        [Key]
        public int Id {get;set;}

        [Required]
        public string Libelle { get; set; }
        [Required]
        [Display(Name = "La salle a t elle un projecteur?")]
        public bool AProjecteur { get; set; }
        [Required]
        public int Capacite { get; set; }
        [Required]
        [Display(Name = "La salle a t elle des prises?")]
        public bool APrises { get; set; }



        public virtual Batiment Batiment { get; set; }
    }
}