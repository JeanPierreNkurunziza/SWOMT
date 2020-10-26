using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Domain.Service
{
    public class FormationService 
    {
        public static void Create(Formation formation)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Formations.Add(formation);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formation = db.Formations.Find(id);
                db.Formations.Remove(formation);
                db.SaveChanges();
            }
        }

        public static List<Formation> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Formations.ToList();
            }
        }

        public static Formation GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formation = db.Formations.Find(id);
                return formation;
            }
        }

        public static void Update(Formation formation)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(formation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static List<Formation> SearchMethodByName(string searchString)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var assets = from s in db.Formations
                             select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    assets = assets.Where(s => s.NomFormation.ToUpper().Contains(searchString.ToUpper())
                                            && s.Description.ToUpper().Contains(searchString.ToUpper())); 
                }

                return assets.ToList();
            }

        }
    }
}
