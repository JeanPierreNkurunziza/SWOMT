using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class FormateurViewModelsService
    {
        public static List<ViewModels.FormateurViewModel> GetFormateurs() 
        {
            List<ViewModels.FormateurViewModel> Liste = new List<ViewModels.FormateurViewModel>();
            var GetListe = FormateurService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.FormateurViewModel vm = new ViewModels.FormateurViewModel()
                {
                    IdFormateur = itemList.IdFormateur,
                    NomFormateur = itemList.NomFormateur,
                    Domaine=itemList.Domaine,                  
                    TelFormateur=itemList.TélFormateur,
                    EmailFormateur=itemList.EmailFormateur,
                    DateEncodage=itemList.DateEncodage,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        public static List<ViewModels.FormateurViewModel> SearchFormateurByName(string searchName)
        {
            List<ViewModels.FormateurViewModel> Liste = new List<ViewModels.FormateurViewModel>();
            var GetListe = FormateurService.SearchMethodByName(searchName);
            foreach (var itemList in GetListe)
            {
                ViewModels.FormateurViewModel vm = new ViewModels.FormateurViewModel()
                {
                    IdFormateur = itemList.IdFormateur,
                    NomFormateur = itemList.NomFormateur,
                    Domaine = itemList.Domaine,
                    TelFormateur = itemList.TélFormateur,
                    EmailFormateur = itemList.EmailFormateur,
                    DateEncodage = itemList.DateEncodage,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
