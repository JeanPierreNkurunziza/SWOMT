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
        public static void Create(string userName, string motDePasse, string userRole) 
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                //db.Utilisateurs.Add(utilisateur);
                //db.SaveChanges();
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

        public static List<Utilisateur> GetAll()
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.ToList();
                return utilisateur;
            }
        }

        public static Utilisateur GetOne(int id)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {
                var utilisateur = db.Utilisateurs.Find(id);
                return utilisateur;
            }
        }

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
        public static string LoginUserNom(string nomUser, string Motdepasse)
        {
            using (TrainingDBEntities db = new TrainingDBEntities())
            {

             return   db.SP_LoginUser(nomUser, Motdepasse).FirstOrDefault(); 
               
            }
        }


    }
}
