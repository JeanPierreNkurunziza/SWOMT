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
        public static List<ViewModels.EvenementViewModel> GetEvenements() 
        {
            List<ViewModels.EvenementViewModel> Liste = new List<ViewModels.EvenementViewModel>();
            var GetListe = EvenementService.GetAll();
            foreach (var itemList in GetListe)
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
