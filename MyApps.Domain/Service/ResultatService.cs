using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyApps.Domain.Service
{
  public  class ResultatService 
    {
        /// <summary>
        /// Méthode pour donner des points à des participants
        /// </summary>
        /// <param name="resultat"></param>
        public static void Create(Resultat resultat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Resultats.Add(resultat);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Méthode pour supprimer des points 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var resultat = db.Resultats.Find(id);
                db.Resultats.Remove(resultat);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// afficher une liste
        /// </summary>
        /// <returns></returns>
        public static List<Resultat> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var resultat = db.Resultats.Include(a => a.ModuleInscription).Include(a => a.Examan);
                return resultat.ToList();
            }
        }
        /// <summary>
        /// afficher un élément de la table résultat 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Resultat GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var resultat = db.Resultats.Find(id);
                return resultat;
            }
        }
        /// <summary>
        /// mettre à jour les éléments de la table
        /// </summary>
        /// <param name="resultat"></param>
        public static void Update(Resultat resultat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(resultat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// afficher le nom de module
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static string GetNomModule(int? idExamen) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                
                var IdExamen = db.Examen.Find(idExamen);                
                var GetIdSiteModule = db.Modules.Find(IdExamen.IdSiteModule);  
               
                var GetName = db.SiteModules.Find(GetIdSiteModule.IdModule);
                var GetNomModule = db.Modules.Find(GetName.IdModule);
                return GetNomModule.NomModule;

            }
        }
        /// <summary>
        /// affiche le nom de participant
        /// </summary>
        /// <param name="IdInscription"></param>
        /// <returns></returns>
        public static string GetNomParticipant(int? IdInscription) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var idInscription = db.ModuleInscriptions.Find(IdInscription);
                var GetName = db.Participants.Find(idInscription.IdParticipant);
                return GetName.NomParticipant; 

            }
        }
        /// <summary>
        /// à partir d'un identifiant on récupere l'id d'un participant
        /// </summary>
        /// <param name="IdInscription"></param>
        /// <returns></returns>
        public static int GetParIdParticipant(int? IdInscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var idInscription = db.ModuleInscriptions.Find(IdInscription);
                var GetName = db.Participants.Find(idInscription.IdParticipant);
                return GetName.IdParticipant;

            }
        }
        /// <summary>
        /// afficher la liste des participant réussis par module
        /// </summary>
        /// <param name="IdExamen"></param>
        /// <returns></returns>
        public static List<Resultat> GetListParticipantRéussi(int IdExamen)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                
                var moduleParticipant = db.Resultats.Where(a => a.IdExamen == IdExamen && a.ParticipantRéussi == true).ToList();
                return moduleParticipant;
            }
        }
        public static List<Resultat> GetListParticipantEchoué(int IdExamen) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleParticipant = db.Resultats.Where(a => a.IdExamen == IdExamen && a.ParticipantRéussi == false).ToList();
                return moduleParticipant;
            }
        }
        public static List<Resultat> GetListModuleEchoué(int IdModuleInscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleParticipant = db.Resultats.Where(a => a.IdModuleInscription == IdModuleInscription && a.ParticipantRéussi == false).ToList();
                return moduleParticipant;
            }
        }
        public static List<Resultat> GetListModuleRéussis(int IdModuleInscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var moduleParticipant = db.Resultats.Where(a => a.IdModuleInscription == IdModuleInscription && a.ParticipantRéussi == true).ToList();
                return moduleParticipant;

            }
        }
        public static int GetIdInscription(int? Idparticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                int idmoduleInscription=0;
                
                var total = from participant in db.Participants
                            join mod in db.ModuleInscriptions
                              on participant.IdParticipant equals mod.IdParticipant
                            join resultat in db.Resultats
                              on mod.IdModuleInscription equals resultat.IdModuleInscription
                            where participant.IdParticipant == Idparticipant 

                            select new { resultat.IdModuleInscription, resultat.IdResultat };

                foreach (var totalcomp in total)
                {

                    idmoduleInscription = totalcomp.IdModuleInscription;
                }
                return idmoduleInscription;

            }
        }
    }
}
