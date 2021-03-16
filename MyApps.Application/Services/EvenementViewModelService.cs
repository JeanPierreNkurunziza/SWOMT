using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class EvenementViewModelService
    {
        /// <summary>
        /// méthode pour récuperer la liste des données pour servir notre interface
        /// </summary>
        /// <returns> returne un liste des évenements </returns>
        public static List<ViewModels.EvenementViewModel> GetEvenements() 
        {
          
            var GetListe = EvenementService.GetAll();
            return GetEvenements(GetListe);
        }

        /// <summary>
        /// affichage des évenements qui n'ont encore depassent 90 jours 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.EvenementViewModel> GetCurrentEvenementsWithin90Days() 
        {
           
            var GetListe = EvenementService.GetListOfCurrentEvenement();
           
            return GetEvenements(GetListe);
        }
        public static List<ViewModels.EvenementViewModel> GetEvenements(List<Infrastructure.DB.Evenement> eventlist)
        {
            List<ViewModels.EvenementViewModel> Liste = new List<ViewModels.EvenementViewModel>();
           
            foreach (var itemList in eventlist)
            {
                ViewModels.EvenementViewModel vm = new ViewModels.EvenementViewModel()
                {
                    IdEvenement = itemList.IdEvenement,
                    IdFormateur = itemList.IdFormateur,
                    Evenement1 = itemList.Evenement1,
                    DateOfEvent = itemList.DateOfEVent,
                    NomFormateur = EvenementService.GetNomFormateur(itemList.IdFormateur)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
