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

        public int BatimentID { get; set; }
        // Navigator
        public virtual CaracteristiqueSalle CaracteristiqueSalle { get; set; }
        public virtual Batiment Batiment { get; set; }
    }
}