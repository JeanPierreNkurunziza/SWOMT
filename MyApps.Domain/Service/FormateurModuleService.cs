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
        /// <summary>
        /// méthode pour affecter le module à un formateur
        /// </summary>
        /// <param name="formateurModule"></param>
        public static void Create(FormateurModule formateurModule)
        {
           
                using (TrainingDBEntities db = new TrainingDBEntities())
                {

                    db.FormateurModules.Add(formateurModule);
                    db.SaveChanges();
                }
                       
        }
        /// <summary>
        /// méthode pour supprimer un élément dans formateur module planning
        /// </summary>
        /// <param name="idFormateurModule"></param>
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
        /// <summary>
        /// méthode pour afficher la liste des modules et formateurs
        /// </summary>
        /// <returns></returns>
        public static List<FormateurModule> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Include(a => a.Module).Include(a => a.Formateur);
                return formateurModule.ToList();
            }
        }
        /// <summary>
        /// récuperer un formateur par module
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public static FormateurModule GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateurModule = db.FormateurModules.Find(id);
                return formateurModule;
            }
        }
        /// <summary>
        /// mettre à jour la liste des formateur par module
        /// </summary>
        /// <param name="formateurModule"></param>
        public static void Update(FormateurModule formateurModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(formateurModule).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer un nom de module
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static string GetNomModule(int? IdModule)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Modules.Find(IdModule);
                return GetName.NomModule;

            }
        }
        /// <summary>
        /// récuperer un nom de formateur via l'identiant dans la table formateur
        /// </summary>
        /// <param name="IdFormateur"></param>
        /// <returns></returns>
        public static string GetNomFormateur(int? IdFormateur) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var GetName = db.Formateurs.Find(IdFormateur);
                return GetName.NomFormateur; 

            }
        }

        /// <summary>
        /// récuperer une liste des moudules par formateur
        /// </summary>
        /// <param name="IdFormateur"></param>
        /// <returns></returns>
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
