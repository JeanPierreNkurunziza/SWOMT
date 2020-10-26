using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Domain.Service
{
    public class UserService
    {
        public static void Create(Utilisateur user) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Utilisateurs.Add(user);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var user = db.Utilisateurs.Find(id);
                db.Utilisateurs.Remove(user);
                db.SaveChanges();
            }
        }

        public static List<Utilisateur> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var user = db.Utilisateurs.ToList();
                return user;
            }
        }

        public static Utilisateur GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var user = db.Utilisateurs.Find(id);
                return user;
            }
        }

        public static void Update(Utilisateur user)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        
        
    }
}
