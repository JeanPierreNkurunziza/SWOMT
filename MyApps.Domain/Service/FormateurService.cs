using MyApps.Domain.Repository;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Domain.Service
{
    public class FormateurService 
    {
        public static void Create(Formateur formateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Formateurs.Add(formateur);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateur = db.Formateurs.Find(id);
                db.Formateurs.Remove(formateur);
                db.SaveChanges();
            }
        }

        public static List<Formateur> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Formateurs.ToList();
            }
        }

        public static Formateur GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateur = db.Formateurs.Find(id);
                return formateur;
            }
        }

        public static void Update(Formateur formateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(formateur).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static List<Formateur> SearchMethodByName(string searchString)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var assets = from s in db.Formateurs
                             select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    assets = assets.Where(s => s.NomFormateur.ToUpper().Contains(searchString.ToUpper()) || s.Domaine.ToUpper().Contains(searchString.ToUpper()));
                }

                return assets.ToList();
            }

        }
    }
}
