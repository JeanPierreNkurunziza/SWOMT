using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Domain.Service
{
   public class UserRolesServices
    {
        public static void Create(UserRole userRole)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.Find(id);
                db.UserRoles.Remove(userRole);
                db.SaveChanges();
            }
        }

        public static List<UserRole> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.ToList();
                return userRole;
            }
        }

        public static UserRole GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.Find(id);
                return userRole;
            }
        }

        public static void Update(UserRole userRole)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(userRole).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
