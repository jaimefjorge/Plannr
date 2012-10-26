using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Plannr.Models
{

    public class PlannrInitializer : DropCreateDatabaseIfModelChanges<PlannrContext>
    {
        protected override void Seed(PlannrContext context)
        {

            var batiment = new Batiment()
            {
                Id = 1,
                Nom = "Polytech"
            };

            context.Batiments.Add(batiment);
            context.SaveChanges();

            var caracteristique = new CaracteristiqueSalle()
            {
                Id = 1,
                APrises = true,
                AProjecteur = true,
                Capacite = 100
            };

            context.CaracteristiquesSalles.Add(caracteristique);
            context.SaveChanges();

            var salle = new List<Salle>()
            {
                new Salle() {
                    Id = 1,
                    Libelle = "202",
                    BatimentID = 1,
                    CaracteristiqueSalle = caracteristique
                },
                new Salle() {
                    Id = 2,
                    Libelle = "201",
                    BatimentID = 1,
                    CaracteristiqueSalle = caracteristique
                }
            };

            salle.ForEach(p => context.Salles.Add(p));
            context.SaveChanges();

            var creneau = new List<Creneau>()
            {
                new Creneau() {
                    Id = 1,
                    HeureDebut = 8,
                    HeureFin = 10,
                    Date = DateTime.Parse("23/10/2012")
                },
                new Creneau() {
                    Id = 2,
                    HeureDebut = 9,
                    HeureFin = 10,
                    Date = DateTime.Parse("24/10/2012")
                }
            };

            creneau.ForEach(c => context.Creneaux.Add(c));
            context.SaveChanges();

            var typeCours = new TypeCours()
            {
                Id = 1,
                Type = "CM"

            };

            context.TypesCours.Add(typeCours);
            context.SaveChanges();

            var responsable = new ResponsableUE()
            {
                UserId = 1,
                UserName = "AnneLaurent",
                ResponsableDepuis = DateTime.Parse("10/01/2009")
            };

            context.ResponsablesUE.Add(responsable);
            context.SaveChanges();

            var enseignant = new Enseignant()
            {
                UserId = 1,
                UserName = "TiberiuStratulat"
            };
            context.Enseignants.Add(enseignant);
            context.SaveChanges();

          

            var ue = new Ue()
            {
                Id = 1,
                ResponsableUe = responsable,
                Libelle = "FLIN201",
                Description = "UE tranquillement sa mere"
            };
            context.Ues.Add(ue);
            context.SaveChanges();

            var matiere = new List<Matiere>() {
                new Matiere() {
                    Id = 1,
                    Ue = ue,
                    Libelle = "Droit"
                },
                new Matiere() {

                    Id = 2,
                    Ue = ue,
                    Libelle = "Oriented Object Engeneering"
                }
             
              };

            matiere.ForEach(x => context.Matieres.Add(x));
            context.SaveChanges();

             var cours = new List<Cours>()
            {
                new Cours() {
                    Id = 1,
                    Libelle = "Initiation au droit - Marques et Brevets",
                    TypeCours = typeCours,
                    Matiere = matiere[0]
                },
                new Cours() {
                    Id = 2,
                    Libelle = "Java",
                    TypeCours = typeCours,
                    Matiere = matiere[1]
                }
            };
             cours.ForEach(x => context.Cours.Add(x));
            context.SaveChanges();

       
                var groupe = new Groupe() {
                    Id = 1,
                    Libelle = "IG5"
                };
                var sous_groupe = new Groupe()
                {
                    Id = 2,
                    Libelle = "IG5 Groupe 1",
                    GroupePere = groupe
                };
       

            context.Groupes.Add(groupe);
            
            context.SaveChanges();

            context.Groupes.Add(sous_groupe);
            context.SaveChanges();

            var enseignement = new List<Enseignement>()
            {
                new Enseignement() {
                    Id = 1,
                    Cours = cours[1],
                    Enseignant = responsable,
                    Groupe = groupe
                },
                new Enseignement() {
                    Id = 2,
                    Cours = cours[0],
                    Enseignant = enseignant,
                    Groupe = sous_groupe
                }
            };

            enseignement.ForEach(x => context.Enseignements.Add(x));
            context.SaveChanges();

            var demande = new List<DemandeReservation>() {
               new DemandeReservation() {
                   Id = 1,
                   Checked = false,
                  Enseignement = enseignement[0],
                  CapaciteNecesaire = 50,
                  BesoinPrises = false,
                  BesoinProjecteur = true,
                  DateVoulue = DateTime.Parse("25/10/2012"),
               
                  DateDemande = DateTime.Parse("22/10/2012")
               },
               new DemandeReservation() {

                   Id = 2,
                   Checked = true,
                   Enseignement = enseignement[1],
                   CapaciteNecesaire = 100,
                   BesoinProjecteur = false,
                   BesoinPrises =false,
                   DateVoulue = DateTime.Parse("25/10/2012"),
                 
                   DateDemande = DateTime.Parse("24/10/2012")
               }
            };

            demande.ForEach(x => context.DemandesReservation.Add(x));
            context.SaveChanges();

            var reservation = new Reservation()
            {
                Id = 1,
                Creneau = creneau[1],
                Enseignement = enseignement[0],
                DateValidation = DateTime.Parse("23/10/2012"),
                ResponsableUe = responsable,
                Salle = salle[0]
            };

            context.Reservations.Add(reservation);
            context.SaveChanges();

            var personne = new Personne()
            {
                UserId = 2,
                UserName = "TestProf"
            };
            context.Personnes.Add(personne);
            context.SaveChanges();

            // Add ResponsableUE role
            WebSecurity.InitializeDatabaseConnection("PlannrContext", "Personne", "UserId", "UserName", true);
            const string respRole = "ResponsableUE";
            const string enseignantRole = "Enseignant";

            if (!Roles.RoleExists(respRole))
            {
                Roles.CreateRole(respRole);
              
            }

            if (!Roles.RoleExists(enseignantRole)) {
                Roles.CreateRole(enseignantRole);
            }

            WebSecurity.CreateAccount("AnneLaurent", "AnneLaurent");
            WebSecurity.CreateAccount("TiberiuStratulat", "TiberiuStratulat");
            
            Roles.AddUserToRole("AnneLaurent", respRole);
            Roles.AddUserToRole("AnneLaurent",enseignantRole);
            Roles.AddUserToRole("TiberiuStratulat", enseignantRole);

            
            
        }

            
    }
}