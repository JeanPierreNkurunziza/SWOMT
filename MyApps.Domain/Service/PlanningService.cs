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
    public class PlanningService 
    {
        public static void Create(Planning planning)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Plannings.Add(planning);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Find(id);
                db.Plannings.Remove(planning);
                db.SaveChanges();
            }
        }

        public static List<Planning> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Include(a => a.Formation);
                return planning.ToList();
            }
        }

        public static Planning GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Find(id);
                return planning;
            }
        }

        public static void Update(Planning planning) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(planning).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static string GetNomFormation(int? IdFormation)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var nomFormation = db.Formations.Find(IdFormation);
                return nomFormation.NomFormation;

            }
        }
    }
}
