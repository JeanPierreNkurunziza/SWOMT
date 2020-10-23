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
        public static void Create(Presence presence)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Presences.Add(presence);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Find(id);
                db.Presences.Remove(presence);
                db.SaveChanges();
            }
        }

        public static List<Presence> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Include(a => a.Participant).Include(a=>a.SiteModule);
                return presence.ToList();
            }
        }

        public static Presence GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var presence = db.Presences.Find(id);
                return presence;
            }
        }

        public static void Update(Presence presence)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(presence).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
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
        public static string GetNomParticipant(int? IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Participants.Find(IdParticipant);
                return GetName.NomParticipant;

            }
        }
        public static List<Presence> GetListParticipantPresentPerModule(int IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
               
                var moduleParticipant = db.Presences.Where(a => a.IdSiteModule == IdSiteModule && a.EstPresent == true).ToList();
                return moduleParticipant;
            }
        }
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
