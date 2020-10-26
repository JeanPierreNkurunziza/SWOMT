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
    public class CertificatService
    {
        public static void Create(Certificat certificat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Certificats.Add(certificat);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Find(id);
                db.Certificats.Remove(certificat);
                db.SaveChanges();
            }
        }

        public static List<Certificat> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Include(a => a.Participant);
                return certificat.ToList();
            }
        }
        public static List<Certificat> GetListPerParticipant(int IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Where(a=>a.IdParticipant==IdParticipant);
                return certificat.ToList();
            }
        }

        public static Certificat GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Find(id);
                return certificat;
            }
        }

        public static void Update(Certificat certificat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(certificat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
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
