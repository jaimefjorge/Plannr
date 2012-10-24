using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class Creneau
    {
        [Key]
        // Pas d'édition sur l'ID
        [ScaffoldColumn(false)]
        public int Id {get;set;}

        [Required]
        public DateTime Date {get;set;}

        // Un cours ne peut pas commencer avant 8h et après 20h
        [Range(8,20)]
        [Required]
        public int HeureDebut {get;set;}
        // Un cours ne peut pas terminer avant 9h et après 22h
        [Range(9,22)]
        [Required]
        public int HeureFin {get;set;}

        // Chaque creneau peut avoir plusieurs reservations

    }
}