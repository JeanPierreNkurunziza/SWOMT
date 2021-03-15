using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyApps.Domain.Service
{
    public class UserService
    {
     /// <summary>
    /// methode pour créer un utlisateur
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="motDePasse"></param>
    /// <param name="userRole"></param>
        public static void Create(string userName, string motDePasse, string userRole) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                //appel d'un procédure stockée pour la création d'un utilisateur 
                db.SP_RegisterUser(userName, motDePasse, userRole);
            }
        }

        public static void Delete(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.Find(id);
                db.Utilisateurs.Remove(utilisateur);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// récuperer une liste des utilisateurs
        /// </summary>
        /// <returns></returns>
        public static List<Utilisateur> GetAll()
        {
            try
            {
                using (TrainingDBEntities db = new TrainingDBEntities())
                {
                    var utilisateur = db.Utilisateurs.ToList();
                    return utilisateur;
                }
            }
            catch (Exception)
            {
                throw new Exception("Vérifier la connection sqlServer !!! La base de données introuvable ");
               // return null;
            }
            
        }
        /// <summary>
        /// methode pour récuperer un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Utilisateur GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.Find(id);
                return utilisateur;
            }
        }
        /// <summary>
        /// méthode pour mettre à jour l'utilisateur
        /// </summary>
        /// <param name="utilisateur"></param>
        public static void Update(Utilisateur utilisateur)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                db.Entry(utilisateur).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void MetàJourUser(int? idUser, string nomUser, string Motdepasse, string rolename)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
              
                 db.SP_UpDate(idUser, nomUser, Motdepasse,rolename);
                
            }
                      
        }
        
        /// <summary>
        /// méthode pour recupere le nom d'un utilisatuer
        /// </summary>
        /// <param name="utilisateurNom"></param>
        /// <returns></returns>
        public static string GetUtilisateurNom(string utilisateurNom)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.FirstOrDefault(a => a.UserName == utilisateurNom);
                return utilisateur.UserName; 
            }
        }

        public static bool GetUtilisateurMotDePasse(string utilisateurNom, string utilisateurMotDePasse)
        {
            using (TrainingDBEntities db = new TrainingDBEntities()) 
            {
               
                var iduser = GetAll();
                foreach( var utilisateurs in iduser)
                {
                    if (utilisateurs.UserName == utilisateurNom && utilisateurs.MotDePasse == utilisateurMotDePasse)
                    {
                        return true;
                    }
                }
              
                return false;
            }
        }
        public static bool LoginUserCheck(string nomUser, string Motdepasse)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                
                foreach (var user in GetAll())
                {
                    var nom = db.SP_LoginUser(nomUser, Motdepasse);
                    if (user.UserName==nom.ToString() )
                    {
                        //if(user.UserName == nomUser && user.MotDePasse== Motdepasse)
                        return true;
                    }
         
                }

                return false;
            }
        }
        /// <summary>
        /// méthode pour récuperer le role d"un utilisateur
        /// </summary>
        /// <param name="utilisateurNom"></param>
        /// <returns></returns>
        public static string GetUtilisateurUserRole(string utilisateurNom)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.FirstOrDefault(a =>a.UserName == utilisateurNom );  
                return utilisateur.UserRole; 
            } 
        }
       
        public static int GetUtilisateurUserId(string utilisateurNom)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.FirstOrDefault(a => a.UserName == utilisateurNom);
                return utilisateur.IdUser;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomUser"></param>
        /// <param name="Motdepasse"></param>
        /// <returns></returns>
        public static string LoginUserNom(string nomUser, string Motdepasse)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

             return   db.SP_LoginUser(nomUser, Motdepasse).FirstOrDefault(); 
               
            }
        }


    }
}
