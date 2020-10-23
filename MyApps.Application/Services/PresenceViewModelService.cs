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

                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
