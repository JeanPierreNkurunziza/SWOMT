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
        public static List<ViewModels.FormateurModuleViewModel> GetFormatuerModules()
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
                {
                    IdFormateurModule=itemList.IdFormateurModule,
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

        public static List<ViewModels.FormateurModuleViewModel> GetModulesPerFormateur(int IdFormateur) 
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetListModulesPerFormateur(IdFormateur);
            foreach (var itemList in GetListe)
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

        public static List<ViewModels.FormateurModuleViewModel> GetFormateurPerModule(int IdModule) 
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetListFormateurPerModules(IdModule); 
            foreach (var itemList in GetListe)
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
    }
}
