using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class PresenceViewModelService
    {
        /// <summary>
        /// mappées les données de presence 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetPresences()
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = PresenceService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
                {
                    IdPresence = itemList.IdPresence,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    DateHeureDePresence = itemList.DateHeureDePresence,
                    EstPresent = itemList.EstPresent,
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// récuperer les participants present par module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListParticipantPresentPerModule( int IdSiteModule)
        {
           
            var GetListe = PresenceService.GetListParticipantPresentPerModule(IdSiteModule);
            return GetListPresencess(GetListe);
        }
        public static List<ViewModels.PresenceViewModel> GetListParticipantAbsentPerModule(int IdSiteModule)
        {
           
            var GetListe = PresenceService.GetListParticipantAbsentPerModule(IdSiteModule); 
           

            return GetListPresencess(GetListe);
        }
        /// <summary>
        /// afficher selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> SearchMethodByName(string searchString)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = GetPresences();
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomParticipant.ToUpper().Contains(searchString.ToUpper()) || s.DateHeureDePresence.ToString().Contains(searchString.ToString()));
            }

            return assets.ToList();
        }
        public static List<ViewModels.PresenceViewModel> GetListPresencess(List<Infrastructure.DB.Presence> listElement)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
           
            foreach (var itemList in listElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
                {
                    IdPresence = itemList.IdPresence,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    DateHeureDePresence = itemList.DateHeureDePresence,
                    EstPresent = itemList.EstPresent,
                    DateDebutModule = PresenceService.GetDateDebut(itemList.IdSiteModule),
                    DateDeFinModule = PresenceService.GetDateFin(itemList.IdSiteModule),
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
