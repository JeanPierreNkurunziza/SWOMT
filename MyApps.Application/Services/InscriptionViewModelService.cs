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
           
            var GetListe = InscriptionService.GetAll();
            
           return GetListElement(GetListe); 
        }
        public static List<ViewModels.InscriptionViewModel> GetListElement (List<Infrastructure.DB.ModuleInscription> listeElement)
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            
            foreach (var itemList in listeElement)
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

        /// <summary>
        /// avec l'identifiant de site module on peut afficher les paricipants concernés
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetParticipantPerModule(int IdSiteModule)
        {
          
            var GetListe = InscriptionService.GetListParticipantPerModule(IdSiteModule);
           
            return GetListElement(GetListe);
        }
        /// <summary>
        /// afficher les données via l'identifiant de participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetListAttentParticipantPerModule(int IdParticipant) 
        {
          
            var GetListe = InscriptionService.GetListAttenteParticipantPerModule(IdParticipant);
           
            return GetListElement(GetListe);
        }
        /// <summary>
        /// la liste des modules selon le partipant 
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetModulePerParticipant(int IdModule)
        {
           
            var GetListe = InscriptionService.GetListModulePerParticipant(IdModule);
           
            return GetListElement(GetListe);
        }
    }
}
