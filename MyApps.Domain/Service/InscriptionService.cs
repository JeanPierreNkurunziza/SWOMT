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
    public class InscriptionService 
    {
        /// <summary>
        /// Méthode pour créer un inscription
        /// </summary>
        /// <param name="inscription"></param>
        public static void Create(ModuleInscription inscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.ModuleInscriptions.Add(inscription);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Méthode pour supprimer une inscription
        /// </summary>
        /// <param name="id"> identifiant d'un élement à supprimer </param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Find(id);
                db.ModuleInscriptions.Remove(inscription);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Méthode pour récuperer la liste des inscriptions 
        /// </summary>
        /// <returns> la liste </returns>
        public static List<ModuleInscription> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Include(a => a.Participant).Include(a => a.SiteModule);
                return inscription.ToList();
            }
        }
        /// <summary>
        /// Méthode pour récuperer une inscription 
        /// </summary>
        /// <param name="id"> à partir d'un identifiant affiche une inscription</param>
        /// <returns>une inscription d'un participant </returns>
        public static ModuleInscription GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var inscription = db.ModuleInscriptions.Find(id);
                return inscription;
            }
        }
        /// <summary>
        /// Méthode pour mettre à jour une inscription
        /// </summary>
        /// <param name="inscription"></param>
        public static void Update(ModuleInscription inscription)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(inscription).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Méthode pour récuperer le nom de module 
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns> nom de module </returns>
        public static string GetNomModule(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdSiteModule = db.SiteModules.Find(IdSiteModule);
                var idmodule= GetIdSiteModule.IdModule;
                var nomModule = db.Modules.Find(idmodule);
                return nomModule.NomModule; 


            }
        }
        /// <summary>
        /// Méthode pour récuperer le nom de participant à inscrire 
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns> le nom de participant</returns>
        public static string GetNomParticipant(int? IdParticipant) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Participants.Find(IdParticipant);
                return GetName.NomParticipant;   

            }
        }
        /// <summary>
        /// Méthode qui récupere l'identité national de participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static long? GetIdNational(int? IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdNational = db.Participants.Find(IdParticipant);
                return GetIdNational.IdNational; 

            }
        }
        /// <summary>
        /// Méthode pour récuperer la liste des modules par participant 
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns> la liste des modules </returns>
        public static List<ModuleInscription> GetListModulePerParticipant(int IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdParticipant == IdParticipant ).ToList();
                return moduleParticipant;
            }
        }
        /// <summary>
        /// Méthode pour récuperer la liste des participants par module  
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ModuleInscription> GetListParticipantPerModule(int IdSiteModule) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                
                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule && a.EstSurListeAttente==false).ToList();
                return moduleParticipant;
            }
        }

        /// <summary>
        /// la liste des participants inscrits sur une liste d'attente
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ModuleInscription> GetListAttenteParticipantPerModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                //var module =db.SiteModules.Find(IdSiteModule);

                var moduleParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule && a.EstSurListeAttente ==true).ToList();
                return moduleParticipant;
            }
        }
        /// <summary>
        /// calculer le nombre total des participant par module 
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns> le nombre total </returns>
        public static int GetNbreParticipantParModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
              
                var TotalParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule);
                 int total = TotalParticipant.Where(a => a.EstSurListeAttente == true).Count();
               
                return total;
            }
        }
        public static int GetNbreTotalParticipantParModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var TotalParticipant = db.ModuleInscriptions.Where(a => a.IdSiteModule == IdSiteModule);
                int total = TotalParticipant.Count();

                return total;
            }
        }
    }
}
