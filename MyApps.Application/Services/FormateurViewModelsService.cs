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
        /// <summary>
        /// récuperer les données des formateurs 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormateurViewModel> GetFormateurs() 
        {
            var GetListe = FormateurService.GetAll(); 
            return GetFormateurListss(GetListe);
        }
        /// <summary>
        /// méthode pour récuperer les données selon la saisie 
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurViewModel> SearchFormateurByName(string searchName)
        {           
            var GetListe = FormateurService.SearchMethodByName(searchName);
           
            return GetFormateurListss(GetListe);
        }
        /// <summary>
        /// récuperer les données des formateurs dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurViewModel> GetFormateurListss(List<Infrastructure.DB.Formateur> listElement )
        {
            List<ViewModels.FormateurViewModel> Liste = new List<ViewModels.FormateurViewModel>();
          
            foreach (var itemList in listElement)
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
