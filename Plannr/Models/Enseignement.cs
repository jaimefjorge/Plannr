using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Enseignement
    {
        [Key]
        public int Id {get;set;}

        // Navigators
        public virtual Cours Cours { get; set; }
        public virtual Enseignant Enseignant { get; set; }
        public virtual Groupe Groupe { get; set; }
        // Helper method
        public string Libelle
        {
            get
            {
    
               return this.Cours.Libelle + " - " + this.Groupe.Libelle;
                
          
            }
        }

        [JsonIgnore]
        public virtual ICollection<DemandeReservation> DemandesReservation { get; set; }

    }
}