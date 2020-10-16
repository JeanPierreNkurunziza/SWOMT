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
        public static void Create(Site site)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Sites.Add(site);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var site = db.Sites.Find(id);
                db.Sites.Remove(site);
                db.SaveChanges();
            }
        }

        public static List<Site> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Sites.ToList();
            }
        }

        public static Site GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var site = db.Sites.Find(id);
                return site;
            }
        }

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
