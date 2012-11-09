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
      

    }
}