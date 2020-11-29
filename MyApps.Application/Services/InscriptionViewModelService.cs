using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class InscriptionViewModelService
    {
       /// <summary>
       /// la méthode pour mapper les données  de modèles inscription. 
       /// </summary>
       /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetInscriptions() 
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            var GetListe = InscriptionService.GetAll(); 
            foreach (var itemList in GetListe)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdModuleInscription = itemList.IdModuleInscription, 
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant, 
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant), 
                    IdNational=InscriptionService.GetIdNational(itemList.IdParticipant),
                    DateInscription = itemList.DateInscription,
                    EstSurListeAttente = itemList.EstSurListeAttente,

                };
                Liste.Add(vm);
            }

            return Liste;
        }

        public static List<ViewModels.InscriptionViewModel> GetParticipantPerModule(int IdSiteModule)
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            var GetListe = InscriptionService.GetListParticipantPerModule(IdSiteModule);
            foreach (var itemList in GetListe)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdModuleInscription = itemList.IdModuleInscription,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    IdNational = InscriptionService.GetIdNational(itemList.IdParticipant),
                    DateInscription = itemList.DateInscription,
                    EstSurListeAttente = itemList.EstSurListeAttente,
                    TotalParticipantPerModule = InscriptionService.GetNbreParticipantParModule(itemList.IdSiteModule), 

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        public static List<ViewModels.InscriptionViewModel> GetListAttentParticipantPerModule(int IdParticipant) 
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            var GetListe = InscriptionService.GetListAttenteParticipantPerModule(IdParticipant);
            foreach (var itemList in GetListe)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdModuleInscription = itemList.IdModuleInscription,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    IdNational = InscriptionService.GetIdNational(itemList.IdParticipant),
                    DateInscription = itemList.DateInscription,
                    EstSurListeAttente = itemList.EstSurListeAttente,
                    TotalParticipantPerModule = InscriptionService.GetNbreParticipantParModule(itemList.IdSiteModule),

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        public static List<ViewModels.InscriptionViewModel> GetModulePerParticipant(int IdModule)
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            var GetListe = InscriptionService.GetListModulePerParticipant(IdModule);
            foreach (var itemList in GetListe)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdModuleInscription = itemList.IdModuleInscription,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    IdNational = InscriptionService.GetIdNational(itemList.IdParticipant),
                    DateInscription = itemList.DateInscription,
                    EstSurListeAttente = itemList.EstSurListeAttente,
                   
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
