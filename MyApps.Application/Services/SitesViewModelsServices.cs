using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class SitesViewModelsServices
    {
        public static List<ViewModels.SiteViewModel> GetSites()
        {
            List<ViewModels.SiteViewModel> Liste = new List<ViewModels.SiteViewModel>();
            var GetListe = SiteService.GetAll(); 
            foreach (var itemList in GetListe) 
            {
                ViewModels.SiteViewModel vm = new ViewModels.SiteViewModel()
                {
                    IdSite = itemList.IdSite,
                    NomSite=itemList.NomSite,
                    AdresseSite=itemList.AdresseSite,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
