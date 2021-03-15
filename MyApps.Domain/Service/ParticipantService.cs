using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyApps.Domain.Service
{
   public class ParticipantService 
    {
        /// <summary>
        /// méthode pour créer un participant
        /// </summary>
        /// <param name="participant"></param>
        public static void Create(Participant participant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Participants.Add(participant);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// methode pour supprimer un participant
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                
                var participant = db.Participants.Find(id);
                
                db.Participants.Remove(participant);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer la lsite des participants
        /// </summary>
        /// <returns></returns>
        public static List<Participant> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Participants.ToList();
            }
        }
        /// <summary>
        /// méthode pour avoir un participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Participant GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var participant = db.Participants.Find(id);
                return participant;
            }
        }
        /// <summary>
        /// méthode pour mettre à jour un participant
        /// </summary>
        /// <param name="participant"></param>
        public static void Update(Participant participant)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(participant).State = System.Data.Entity.EntityState.Modified;                  
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour faire une recherche via un text
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<Participant> SearchParticipantByName(string searchString)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var assets = from s in db.Participants 
                             select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    assets = assets.Where(s => s.NomParticipant.ToUpper().Contains(searchString.ToUpper())); 
                }

                return assets.ToList();
            }

        }
      
    }
}
