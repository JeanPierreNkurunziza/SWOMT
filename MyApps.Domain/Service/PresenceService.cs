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
        public static string GetNomModule(int? IdModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Modules.Find(IdModule);
                return GetName.NomModule;

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
    }
}
