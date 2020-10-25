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
        public static List<ViewModels.PresenceViewModel> GetListParticipantPresentPerModule( int IdSiteModule)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = PresenceService.GetListParticipantPresentPerModule(IdSiteModule);
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
                    DateDebutModule = PresenceService.GetDateDebut(itemList.IdSiteModule),
                    DateDeFinModule = PresenceService.GetDateFin(itemList.IdSiteModule),
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        public static List<ViewModels.PresenceViewModel> GetListParticipantAbsentPerModule(int IdSiteModule)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = PresenceService.GetListParticipantAbsentPerModule(IdSiteModule); 
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
                    DateDebutModule=PresenceService.GetDateDebut(itemList.IdSiteModule),
                    DateDeFinModule=PresenceService.GetDateFin(itemList.IdSiteModule),
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
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
    }
}
