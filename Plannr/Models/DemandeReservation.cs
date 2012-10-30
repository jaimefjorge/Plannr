using Newtonsoft.Json;
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

        public bool BesoinProjecteur { get; set; }
        public bool BesoinPrises { get; set; }
        [Required]
        public int CapaciteNecessaire { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateVoulue { get; set; }

        [JsonIgnore]
        public virtual CreneauHoraire CreneauSouhaite { get; set; }


        // Return "simple" object for API
        public string Enseignement_Libelle
        {
            get
            {
                return this.Enseignement.Libelle;
            }
        }

        public int Enseignement_Id
        {
            get
            {
                return this.Enseignement.Id;
            }
        
        }

        public string CreneauSouhaite_Libelle {
            get {
                return this.CreneauSouhaite.HeureConcat;
            }
        }


        public int ReservationAssociee_Id
        {
            get
            {
                if (this.ReservationAssociee != null)
                {
                    return this.ReservationAssociee.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Navigators only - table de jointure
        [JsonIgnore]
        public virtual Enseignement Enseignement {get;set;}
        [JsonIgnore]
        public virtual Reservation ReservationAssociee { get; set; }

       
    }
}