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
  public  class PresenceService 
    {
        /// <summary>
        /// Méthode pour crééer un élément dans la table présence 
        /// </summary>
        /// <param name="presence"></param>
        public static void Create(Presence presence)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Presences.Add(presence);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Méthode pour supprimer un élément dans la table présence
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Find(id);
                db.Presences.Remove(presence);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Méthode pour récuperer la liste des présences
        /// </summary>
        /// <returns></returns>
        public static List<Presence> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Include(a => a.Participant).Include(a=>a.SiteModule);
                return presence.ToList();
            }
        }
        /// <summary>
        /// Méthode pour récuperer une présence à partir d'un identifiant 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Presence GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Find(id);
                return presence;
            }
        }
        /// <summary>
        /// méthode pour mettre à jour la liste des présences
        /// </summary>
        /// <param name="presence"></param>
        public static void Update(Presence presence)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(presence).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Méthode pour récuperer le nom de module à partir l'identifiant d'un module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static string GetNomModule(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdSiteModule = db.SiteModules.Find(IdSiteModule);
                var idmodule = GetIdSiteModule.IdModule;
                var nomModule = db.Modules.Find(idmodule);
                return nomModule.NomModule;

            }
        }
        /// <summary>
        /// récuperer la date de debut de module dans la table planning 
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static DateTime? GetDateDebut(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdSiteModule = db.SiteModules.Find(IdSiteModule);
              
                return GetIdSiteModule.DateDebutModule;

            }
        }
        /// <summary>
        /// Récuperation de la date de fin pour chaque module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static DateTime? GetDateFin(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdSiteModule = db.SiteModules.Find(IdSiteModule);

                return GetIdSiteModule.DateFinModule;

            }
        }
        /// <summary>
        /// Recuperation de nom de participant 
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static string GetNomParticipant(int? IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Participants.Find(IdParticipant);
                return GetName.NomParticipant;

            }
        }
        /// <summary>
        /// recupération de numéro national d'un participant 
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static long? GetIdNational(int? IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Participants.Find(IdParticipant);
                return GetName.IdNational; 

            }
        }
        /// <summary>
        /// la recupération de liste des modules par participant
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<Presence> GetListParticipantPresentPerModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
               
                var moduleParticipant = db.Presences.Where(a => a.IdSiteModule == IdSiteModule && a.EstPresent == true).ToList();
                return moduleParticipant;
            }
        }
        /// <summary>
        /// Méthode pour recupérer la liste des participants absents par module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<Presence> GetListParticipantAbsentPerModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var moduleParticipant = db.Presences.Where(a => a.IdSiteModule == IdSiteModule && a.EstPresent == false).ToList(); 
                return moduleParticipant;
            }
        }
    }
}
