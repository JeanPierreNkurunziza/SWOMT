using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Domain.Service
{
    public class SiteService 
    {
        /// <summary>
        /// Méthode pour créer une site service 
        /// </summary>
        /// <param name="site"></param>
        public static void Create(Site site)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Sites.Add(site);
                db.SaveChanges();
            }
        }
/// <summary>
/// méthode pour supprimer un service site
/// </summary>
/// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var site = db.Sites.Find(id);
                db.Sites.Remove(site);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer la liste des sites
        /// </summary>
        /// <returns></returns>

        public static List<Site> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Sites.ToList();
            }
        }
        /// <summary>
        /// récuperer un site via don identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Site GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var site = db.Sites.Find(id);
                return site;
            }
        }
        /// <summary>
        /// mettre à jour un site service
        /// </summary>
        /// <param name="site"></param>
        public static void Update(Site site)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(site).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
