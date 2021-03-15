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
        /// <summary>
        /// méthode pour créer un examen
        /// </summary>
        /// <param name="examen"></param>
        public static void Create(Examan examen) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Examen.Add(examen);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthod pour supprimer un examen
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var examen = db.Examen.Find(id);
                db.Examen.Remove(examen);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour avoir la liste des examens programmés
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// méthode pour mettre à jour la liste des examens
        /// </summary>
        /// <param name="examen"></param>
        public static void Update(Examan examen)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(examen).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer un nom de module pour un examen programmé
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static string GetNomModule(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.SiteModules.Find(IdSiteModule);
                var GetNomModule = db.Modules.Find(GetName.IdModule);
                return GetNomModule.NomModule; 

            }
        }
        /// <summary>
        /// récuperer le nom de formateur programmé un examen 
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static string GetNomFormateur(int? IdSiteModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var getIdSiteModule = db.SiteModules.Find(IdSiteModule);
                var IdFormateurModule = getIdSiteModule.IdFormateurModule;
                var GetIdFormateurModule = db.FormateurModules.Find(IdFormateurModule);
                var IdFormateur = GetIdFormateurModule.IdFormateur;
                var nomFormateur = db.Formateurs.Find(IdFormateur);
                return nomFormateur.NomFormateur; 

            }
        }

    }
}
