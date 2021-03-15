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
        /// <summary>
        /// méthode pour créer un certificat 
        /// </summary>
        public static void Create(Certificat certificat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Certificats.Add(certificat);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// methode pour supprimer un certificat
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Find(id);
                db.Certificats.Remove(certificat);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// methode pour récuperer la lsite des certificat
        /// </summary>
        /// <returns></returns>

        public static List<Certificat> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Include(a => a.Participant);
                return certificat.ToList();
            }
        }
        /// <summary>
        /// mathode pour avour une liste des participants p
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static List<Certificat> GetListPerParticipant(int IdParticipant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Where(a=>a.IdParticipant==IdParticipant);
                return certificat.ToList();
            }
        }
        /// <summary>
        /// methode pour avoir un certificat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Certificat GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var certificat = db.Certificats.Find(id);
                return certificat;
            }
        }
        /// <summary>
        /// mettre à jour a liste des certificat
        /// </summary>
        /// <param name="certificat"></param>
        public static void Update(Certificat certificat)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(certificat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer le nom d'un participant 
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
        
    }
}
