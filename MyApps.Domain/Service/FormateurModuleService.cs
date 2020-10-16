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
            try
            {
                using (TrainingDBEntities db = new TrainingDBEntities())
                {

                    db.FormateurModules.Add(formateurModule);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (CheckIfItemsExists(formateurModule.IdFormateur, formateurModule.IdModule))
                {
                    Environment.Exit(-1); 
                }
            }
            
        }

        public static void Delete(int idFormateur, int idModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Find(idFormateur, idModule);
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

        // méthode qui ne fonctionne pas !!!!! à vérifier pour l'utiliser dans la classe de xmal
        public static bool CheckIfItemsExists(int idFormateur, int idModule)
        {
            using(TrainingDBEntities db= new TrainingDBEntities())
            {
                var items = db.FormateurModules.Where((a => a.IdFormateur == idFormateur && a.IdModule == idModule));
                return true;
            }
           
        }
    }
}
