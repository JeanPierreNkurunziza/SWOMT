using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
  public  class FormationViewModelsServices
    {
        /// <summary>
        /// mapping datas of the view model and view
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormationViewModel> GetFormations()
        {
            List<ViewModels.FormationViewModel> FormationListe = new List<ViewModels.FormationViewModel>();
            var ListeFormation = FormationService.GetAll(); 
            foreach (var formation in ListeFormation)
            {
                ViewModels.FormationViewModel vm = new ViewModels.FormationViewModel() 
                {
                    IdFormation= formation.IdFormation,
                    NomFormation= formation.NomFormation,
                    Description= formation.Description,

                };
                FormationListe.Add(vm);
            }

            return FormationListe;
        }
        public static List<ViewModels.FormationViewModel> GetSearchByName(string searchString)
        {
            List<ViewModels.FormationViewModel> FormationListe = new List<ViewModels.FormationViewModel>();
            var ListeFormation = FormationService.SearchMethodByName(searchString);  // appel d'un méthode de rechercher par nom 
            foreach (var formation in ListeFormation)
            {
                ViewModels.FormationViewModel vm = new ViewModels.FormationViewModel()
                {
                    IdFormation = formation.IdFormation,
                    NomFormation = formation.NomFormation,
                    Description = formation.Description,

                };
                FormationListe.Add(vm);
            }

            return FormationListe;
        }
    }
}
