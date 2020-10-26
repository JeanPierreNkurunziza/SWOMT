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
   public class SitePlanningService  
    {
        public static void Create(SiteModule sitePlanning)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.SiteModules.Add(sitePlanning);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var sitePlanning = db.SiteModules.Find(id);
                db.SiteModules.Remove(sitePlanning);
                db.SaveChanges();
            }
        }

        public static List<SiteModule> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var sitePlanning = db.SiteModules.Include(a => a.Module).Include(a => a.Site);
                return sitePlanning.ToList(); 
            }
        }

        public static SiteModule GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var sitePlanning = db.SiteModules.Find(id);
                return sitePlanning;
            }
        }

        public static void Update(SiteModule sitePlanning)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(sitePlanning).State = System.Data.Entity.EntityState.Modified;
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
        public static string GetNomSite(int? IdSite) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Sites.Find(IdSite);
                return GetName.NomSite; 

            }
        }
        public static string GetNomFormateur(int? IdFormateurModule) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetIdFormateurModule = db.FormateurModules.Find(IdFormateurModule);
                var IdFormateur = GetIdFormateurModule.IdFormateur;  
                var nomFormateur = db.Formateurs.Find(IdFormateur);
                return nomFormateur.NomFormateur; 

            }
        }
        public static List<SiteModule> GetListModulesPerSite(int IdSite)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleSite = db.SiteModules.Where(a => a.IdSite == IdSite).ToList();
                return moduleSite;
            }
        }

        public static List<SiteModule> GetListSitePerModule(int IdModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleSite = db.SiteModules.Where(a => a.IdModule == IdModule).ToList();
                return moduleSite;
            }
        }
        public static List<SiteModule> GetListModuleEncours()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleSite = db.SiteModules.Where(a => a.DateFinModule >= DateTime.Now && a.DateDebutModule <=DateTime.Now).ToList();
                return moduleSite;
            }
        }
        public static List<SiteModule> GetListModulePlanifies() 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var moduleSite = db.SiteModules.Where(a => a.DateDebutModule >= DateTime.Now).ToList();
                return moduleSite;
            }
        }

    }
}
