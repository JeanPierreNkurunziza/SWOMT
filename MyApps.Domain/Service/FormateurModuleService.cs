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
   public class FormateurModuleService 
    {
        public static void Create(FormateurModule formateurModule)
        {
           
                using (TrainingDBEntities db = new TrainingDBEntities())
                {

                    db.FormateurModules.Add(formateurModule);
                    db.SaveChanges();
                }
                       
        }

        public static void Delete(int idFormateurModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Find(idFormateurModule); 
                if(formateurModule != null)
                {
                    db.FormateurModules.Remove(formateurModule);
                    db.SaveChanges();

                }
               
            }
        }

        public static List<FormateurModule> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Include(a => a.Module).Include(a => a.Formateur);
                return formateurModule.ToList();
            }
        }

        public static FormateurModule GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Find(id);
                return formateurModule;
            }
        }

        public static void Update(FormateurModule formateurModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(formateurModule).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static string GetNomModule(int? IdModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Modules.Find(IdModule);
                return GetName.NomModule;

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


        public static List<FormateurModule> GetListModulesPerFormateur(int IdFormateur)
        { 
            using(TrainingDBEntities db= new TrainingDBEntities())
            {
                var moduleFormateur = db.FormateurModules.Where(a => a.IdFormateur == IdFormateur).ToList();
                return moduleFormateur;
            }
        }

        public static List<FormateurModule> GetListFormateurPerModules(int IdModule) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Where(a => a.IdModule == IdModule).ToList(); 
                return formateurModule;
            }
        }


    }
}
