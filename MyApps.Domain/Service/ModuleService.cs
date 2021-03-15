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
        /// <summary>
        /// méthode pour créer module
        /// </summary>
        /// <param name="module"></param>
        public static void Create(Module module)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Modules.Add(module);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour supprimer un module
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Find(id);
                db.Modules.Remove(module);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer la liste de module
        /// </summary>
        /// <returns></returns>
        public static List<Module> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Include(a => a.Formation).OrderBy(a=>a.NomModule);
                return module.ToList();
            }
        }
        /// <summary>
        /// méthode pour avoir un module 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Module GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Find(id);
                return module;
            }
        }
        /// <summary>
        /// mettre à jour un module
        /// </summary>
        /// <param name="module"></param>
        public static void Update(Module module)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(module).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer un nom de formation par module
        /// </summary>
        /// <param name="IdFormation"></param>
        /// <returns></returns>
        public static string GetNomFormation(int? IdFormation) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Formations.Find(IdFormation);
                return GetName.NomFormation; 

            }
        }
        /// <summary>
        /// méthode pour faire une recherche par un mot
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<Module> SearchMethodByName(string searchString)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var assets = from s in db.Modules
                             select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    assets = assets.Where(s => s.NomModule.ToUpper().Contains(searchString.ToUpper())); 
                }

                return assets.ToList();
            }

        }
        public static List<Module> GetModulePerFormation(int IdFormation)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var module = db.Modules.Where(a=>a.IdFormation==IdFormation).ToList();
                return module;
            }

        }
    }
}
