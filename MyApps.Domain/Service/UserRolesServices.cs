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
        /// <summary>
        /// méthode pour créér un role
        /// </summary>
        /// <param name="userRole"></param>
        public static void Create(UserRole userRole)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour supprimer un role
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.Find(id);
                db.UserRoles.Remove(userRole);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour récuperer une liste de roles
        /// </summary>
        /// <returns></returns>
        public static List<UserRole> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.ToList();
                return userRole;
            }
        }
        /// <summary>
        /// méthode pour récuperer un role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserRole GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var userRole = db.UserRoles.Find(id);
                return userRole;
            }
        }
        /// <summary>
        /// methode pour mettre à jour un role
        /// </summary>
        /// <param name="userRole"></param>
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
