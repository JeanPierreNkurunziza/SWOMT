﻿using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class SitePlanningViewModelService
    {
        public static List<ViewModels.SitePlanningViewModel> GetSitePlanning()
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            var GetListe = SitePlanningService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
                {
                    IdSiteModule = itemList.IdSiteModule,
                    IdSite = itemList.IdSite,
                    NomSite = SitePlanningService.GetNomSite(itemList.IdSite),
                    IdModule = itemList.IdModule,
                    NomModule = SitePlanningService.GetNomModule(itemList.IdModule),
                    DateDebutModule = itemList.DateDebutModule,
                    DateFinModule = itemList.DateFinModule
                };
                Liste.Add(vm);
            }

            return Liste;
        }

        public static List<ViewModels.SitePlanningViewModel> GetModulesPerSite(int IdSite)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            var GetListe = SitePlanningService.GetListModulesPerSite(IdSite);
            foreach (var itemList in GetListe)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
                {
                    IdSiteModule = itemList.IdSiteModule,
                    IdSite = itemList.IdSite,
                    NomSite = SitePlanningService.GetNomSite(itemList.IdSite),
                    IdModule = itemList.IdModule,
                    NomModule = SitePlanningService.GetNomModule(itemList.IdModule),
                    DateDebutModule = itemList.DateDebutModule,
                    DateFinModule = itemList.DateFinModule

                };
                Liste.Add(vm);
            }

            return Liste;
        }

        public static List<ViewModels.SitePlanningViewModel> GetSitePerModule(int IdModule)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            var GetListe = SitePlanningService.GetListSitePerModule(IdModule);
            foreach (var itemList in GetListe)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
                {
                    IdSiteModule = itemList.IdSiteModule,
                    IdSite = itemList.IdSite,
                    NomSite = SitePlanningService.GetNomSite(itemList.IdSite),
                    IdModule = itemList.IdModule,
                    NomModule = SitePlanningService.GetNomModule(itemList.IdModule),
                    DateDebutModule = itemList.DateDebutModule,
                    DateFinModule = itemList.DateFinModule

                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}