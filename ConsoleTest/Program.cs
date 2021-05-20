using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var users = MyApps.Domain.Service.UserService.GetAll();
            //foreach(var user in users)
            //{
            //    Console.WriteLine(user.UserName);
            //    Console.ReadLine();
            //}
            MyApps.Infrastructure.DB.Utilisateur utilisateur = new MyApps.Infrastructure.DB.Utilisateur();
            utilisateur.UserName = "Admin";
            utilisateur.MotDePasse = MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.CreateHash("Admin1010.");
            utilisateur.UserRole = "Admin";
            MyApps.Domain.Service.UserService.Ajouter(utilisateur);
           // var nom= MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.verifyCrypto("Admin","admin");

            Console.WriteLine("l'ajout à été réalisé");
            //////var nom=  SWOMT.SWOMTAUTHENTIFICATION.PasswordStorage.verifCrypto("hinda","hinda");
            //////  Console.WriteLine(nom);
            Console.ReadLine(); 
        } 
    }
}
