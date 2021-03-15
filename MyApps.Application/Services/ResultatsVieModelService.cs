using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class ResultatsVieModelService
    {
        /// <summary>
        /// récuperer la liste des éléments à traiter
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetResultatsList(List<Infrastructure.DB.Resultat> listeElement)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            
            foreach (var itemList in listeElement)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
                {
                    IdResultat = itemList.IdResultat,
                    IdExamen = itemList.IdExamen,
                    IdModuleInscription = itemList.IdModuleInscription,
                    NomModule = ResultatService.GetNomModule(itemList.IdExamen),
                    NomParticipant = ResultatService.GetNomParticipant(itemList.IdModuleInscription),
                    Points = itemList.Points,
                    EstPresent = itemList.EstPresent,
                    ParticipantRéussi = itemList.ParticipantRéussi
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// récuperer une liste des résultats 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetResultats() 
        {
            
            var GetListe = ResultatService.GetAll();
            
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une lsite des participants réussis
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListParticipantRéussi(int idExamen) 
        {
           
            var GetListe = ResultatService.GetListParticipantRéussi(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des participant échouées
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListParticipantEchoué(int idExamen) 
        {
            
            var GetListe = ResultatService.GetListParticipantEchoué(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules per inscription
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListModulesEchoué(int idModuleInscription)  
        {
           
            var GetListe = ResultatService.GetListModuleEchoué(idModuleInscription);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules réussis par inscription 
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListModulesRéussis(int idModuleInscription) 
        {
            //List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListModuleRéussis(idModuleInscription);
            return GetResultatsList(GetListe);
        }

    }
}
