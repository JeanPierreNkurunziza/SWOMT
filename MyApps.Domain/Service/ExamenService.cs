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
   public class ExamenService 
    {
        public static void Create(Examan examen) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Examen.Add(examen);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var examen = db.Examen.Find(id);
                db.Examen.Remove(examen);
                db.SaveChanges();
            }
        }

        public static List<Examan> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var examen = db.Examen.Include(a => a.SiteModule);
                return examen.ToList();
            }
        }

        public static Examan GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var examen = db.Examen.Find(id);
                return examen;
            }
        }

        public static void Update(Examan examen)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(examen).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static string GetNomModule(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.SiteModules.Find(IdSiteModule);
                var GetNomModule = db.Modules.Find(GetName.IdModule);
                return GetNomModule.NomModule; 

            }
        }
    }
}
