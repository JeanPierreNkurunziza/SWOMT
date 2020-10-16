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
    public class ModuleService 
    {
        public static void Create(Module module)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Modules.Add(module);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Find(id);
                db.Modules.Remove(module);
                db.SaveChanges();
            }
        }

        public static List<Module> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Include(a => a.Formation);
                return module.ToList();
            }
        }

        public static Module GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Find(id);
                return module;
            }
        }

        public static void Update(Module module)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(module).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static string GetNomFormation(int? IdFormation) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Formations.Find(IdFormation);
                return GetName.NomFormation; 

            }
        }
    }
}
