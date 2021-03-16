using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyApps.Domain.Service
{
   public class EvenementService
    {
        /// <summary>
        /// methode pour créer un evenement 
        /// </summary>
        /// <param name="evenement"></param>
        public static void Create(Evenement evenement)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
       
                db.Evenements.Add(evenement); 
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour supprimer un évenement
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Find(id);
                db.Evenements.Remove(evenement);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer la liste des évenements
        /// </summary>
        /// <returns></returns>
        public static List<Evenement> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Include(a => a.Formateur); 
                return evenement.ToList();
            }
        }
        /// <summary>
        /// méthode pour récuperer un evenement via ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Evenement GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Find(id);
                return evenement;
            }
        }

       /// <summary>
       /// mettre à jour l'evenement
       /// </summary>
       /// <param name="evenement"></param>
        public static void Update(Evenement evenement)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(evenement).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer un nom de personne posté un evenement
        /// </summary>
        /// <param name="IdFormateur"></param>
        /// <returns></returns>
        public static string GetNomFormateur(int? IdFormateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Formateurs.Find(IdFormateur);
                
                return GetName.NomFormateur; 

            }
        }
        /// <summary>
        /// méthode pour récuperer la liste des évenement les plus récentes
        /// </summary>
        /// <returns></returns>
        public static List<Evenement> GetListOfCurrentEvenement() 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                DateTime weekago = DateTime.Now.AddDays(-90);
              
                var moduleParticipant = db.Evenements.Where(a => a.DateOfEVent>=weekago).OrderByDescending(a => a.DateOfEVent);
                return moduleParticipant.ToList(); 
            }
        }
    }
}
