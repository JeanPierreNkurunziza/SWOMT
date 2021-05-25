using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class FormateurModuleViewModelService
    {
        /// <summary>
        /// méthode pour mapper les modules des formateurs avec les objets concernés
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateursModules(List<Infrastructure.DB.FormateurModule> listeElement)
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
          
            foreach (var itemList in listeElement)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
                {
                    IdFormateurModule = itemList.IdFormateurModule,
                    IdFormateur = itemList.IdFormateur,
                    IdModule = itemList.IdModule,
                    NomModule = FormateurModuleService.GetNomModule(itemList.IdModule),
                    NomFormateur = FormateurModuleService.GetNomFormateur(itemList.IdFormateur),
                    VersionModule = itemList.VersionModule,

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// méthode pour récupere les données des formateurs et leurs modules concernés 
        /// </summary>
        /// <returns> les formateurs et leurs modules </returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormatuerModules()
        {
           
            var GetListe = FormateurModuleService.GetAll();


            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// l'affichage de liste des modules par formateurs
        /// </summary>
        /// <param name="IdFormateur"></param>
        /// <returns></returns>

        public static List<ViewModels.FormateurModuleViewModel> GetModulesPerFormateur(int IdFormateur) 
        {
            var GetListe = FormateurModuleService.GetListModulesPerFormateur(IdFormateur);
            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// méthode pour récuperer les formateurs selon le module ciblé 
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateurPerModule(int IdModule) 
        {
            var GetListe = FormateurModuleService.GetListFormateurPerModules(IdModule);
            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// Les données de chaque module récuperer dans la DB
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetModulesList(List<Infrastructure.DB.Module> listeElement)
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            foreach (var itemList in listeElement)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
                {
                    IdModule = itemList.IdModule,
                    IdFormation = itemList.IdFormation,
                    NomModule = itemList.NomModule,
                    NomFormation = ModuleService.GetNomFormation(itemList.IdFormation),
                    CreditModule = itemList.CreditModule,
                    NombrePrévu = itemList.NombrPrévu
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// récuperer les modules
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetModules()
        {
            var GetListe = ModuleService.GetAll();
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// afficher selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> SearchModuleByName(string searchString)
        {
            var GetListe = ModuleService.SearchMethodByName(searchString);
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// la liste des participants 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateurs()
        {

            var GetListe = FormateurService.GetAll();
            return GetFormateurListss(GetListe);
        }
        /// <summary>
        /// méthode pour afficher dans le résultat trouvé par la recherche
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> SearchFormateurByName(string searchName)
        {
            var GetListe = FormateurService.SearchMethodByName(searchName);
            return GetFormateurListss(GetListe);
        }
        /// <summary>
        /// Récuperer les données de formateur dans la base de données
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateurListss(List<Infrastructure.DB.Formateur> listElement)
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();

            foreach (var itemList in listElement)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
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
        /// <summary>
        /// les données pour les formation planifiés 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormations()
        {
            List<ViewModels.FormateurModuleViewModel> FormationListe = new List<ViewModels.FormateurModuleViewModel>();
            var ListeFormation = FormationService.GetAll();
            foreach (var formation in ListeFormation)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
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
