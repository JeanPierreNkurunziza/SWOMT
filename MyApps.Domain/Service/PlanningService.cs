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
        /// <summary>
        /// methode pour creer un planning
        /// </summary>
        /// <param name="planning"></param>
        public static void Create(Planning planning)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Plannings.Add(planning);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour supprimer un planning
        /// </summary>
        /// <param name="id"></param>

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Find(id);
                db.Plannings.Remove(planning);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer la liste de planning
        /// </summary>
        /// <returns></returns>
        public static List<Planning> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Include(a => a.Formation);
                return planning.ToList();
            }
        }
        /// <summary>
        /// méthode pour récuperer un planning
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Planning GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var planning = db.Plannings.Find(id);
                return planning;
            }
        }
        /// <summary>
        /// méthode pour mettre à jour le planning
        /// </summary>
        /// <param name="planning"></param>
        public static void Update(Planning planning) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(planning).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer le nom de formation
        /// </summary>
        /// <param name="IdFormation"></param>
        /// <returns></returns>
        public static string GetNomFormation(int? IdFormation)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                var nomFormation = db.Formations.Find(IdFormation);
                return nomFormation.NomFormation;

            }
        }
        /// <summary>
        /// méthode pour récuperer la liste de planning pour deux ans
        /// </summary>
        /// <returns></returns>
        public static List<Planning> GetListOfPlanningFormationWithinAcademicYear() 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                DateTime nextYear = DateTime.Now.AddYears(+1);
                DateTime currentYear =new  DateTime();
                
                //var siteplanning = db.SiteModules.Find(IdSiteModule);
                // var idSiteModule = siteplanning.IdSiteModule;
                var moduleParticipant = db.Plannings.Where(a =>a.DateFormation.Year==currentYear.Year || a.DateFormation <= nextYear).OrderByDescending(a => a.DateFormation);
                return moduleParticipant.ToList();
            }
        }
    }
}
