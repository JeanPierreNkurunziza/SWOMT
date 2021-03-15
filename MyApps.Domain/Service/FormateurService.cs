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
        /// <summary>
        /// methode pour creer un formateur
        /// </summary>
        /// <param name="formateur"></param>
        public static void Create(Formateur formateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Formateurs.Add(formateur);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour supprimer un formateur
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateur = db.Formateurs.Find(id);
                db.Formateurs.Remove(formateur);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour avoir la liste des formateurs
        /// </summary>
        /// <returns></returns>
        public static List<Formateur> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

                return db.Formateurs.ToList();
            }
        }
        /// <summary>
        /// méthode pour récuperer un seul foramteur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Formateur GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateur = db.Formateurs.Find(id);
                return formateur;
            }
        }
        /// <summary>
        /// méthode pour mettre à jour un formateur
        /// </summary>
        /// <param name="formateur"></param>
        public static void Update(Formateur formateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(formateur).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// méthode pour faire une recherche via le nom de formateur
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
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
        public static bool GetFormateurNom(string FormateurNom)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var formateur = db.Formateurs.FirstOrDefault(a => a.NomFormateur == FormateurNom);
                if(formateur!= null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
