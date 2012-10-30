using Newtonsoft.Json;
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

        [Required]
        public DateTime Date { get; set; }

        public string Creneau_Libelle {
            get{
                return this.Creneau.HeureConcat;
            }
        }

        public string Enseignement_Libelle
        {
            get
            {
                return this.Enseignement.Libelle;
            }
        }

        public string Salle_Libelle
        {
            get
            {
                return this.Salle.Libelle;
            }
        }

        [JsonIgnore]
        public virtual CreneauHoraire Creneau { get; set; }
        [JsonIgnore]
        public virtual Enseignement Enseignement { get; set; }
        [JsonIgnore]
        public virtual Salle Salle { get; set; }
        // Le responsable qui valide la resa
        [JsonIgnore]
        public virtual ResponsableUE ResponsableUe { get; set; }

    }
}