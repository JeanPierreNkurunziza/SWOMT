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
        public static void Create(Evenement evenement)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
       
                db.Evenements.Add(evenement); 
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Find(id);
                db.Evenements.Remove(evenement);
                db.SaveChanges();
            }
        }

        public static List<Evenement> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Include(a => a.Formateur); 
                return evenement.ToList();
            }
        }

        public static Evenement GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var evenement = db.Evenements.Find(id);
                return evenement;
            }
        }

       
        public static void Update(Evenement evenement)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(evenement).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static string GetNomFormateur(int? IdFormateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Formateurs.Find(IdFormateur);
                
                return GetName.NomFormateur; 

            }
        }
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
