using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApps.Domain.Service;
using System.Data.Entity; 



namespace JobConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var participant = new ParticipantService();
            var examen = ResultatService.GetAll();

            var utilisateur = UserService.GetAll();
            
            string nom = UserService.LoginUserNom("Hinda", "hinda");
            if (nom!=null)
            {
                Console.WriteLine(nom);
            }
            else
            {
                Console.WriteLine("le nom n'existe pas");
            }
            var formateur = FormateurService.GetAll();
            foreach (var formateurs in formateur)
            {
                if ("Christine" == formateurs.NomFormateur)
                {
                    //idFormateurSelected = 0;

                    Console.WriteLine("il n'existe ");
                }
            }

            Console.ReadLine();
            //participant.Delete(30);

            //Console.WriteLine("the delete success");
            //Console.ReadLine();


            //foreach (var etudiant in participant.GetAll())
            //{
            //    Console.WriteLine(etudiant.NomParticipant);
            //    Console.ReadLine();
            //}
            //foreach (var etudiant in examen)
            //{
            //   // Console.WriteLine(etudiant.IdExamen);
            //   // var idModule = etudiant.IdExamen;
            //    Console.WriteLine("{0} - {1} - {2} - {3} - {4} ", etudiant.IdResultat, etudiant.IdExamen, ResultatService.GetNomModule(etudiant.IdExamen),
            //                                                      etudiant.IdModuleInscription, ResultatService.GetNomParticipant(etudiant.IdModuleInscription));
            //    Console.ReadLine();
            //}
        }
    }
}
