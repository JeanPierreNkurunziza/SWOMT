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
        public static List<ViewModels.ResultatViewModel> GetResultats() 
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetAll();
            foreach (var itemList in GetListe)
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
                    ParticipantRéussi=itemList.ParticipantRéussi 
                };
                Liste.Add(vm);
            }

            return Liste;
        }

        public static List<ViewModels.ResultatViewModel> GetListParticipantRéussi(int idExamen) 
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListParticipantRéussi(idExamen);
            foreach (var itemList in GetListe)
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

        public static List<ViewModels.ResultatViewModel> GetListParticipantEchoué(int idExamen) 
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListParticipantEchoué(idExamen); 
            foreach (var itemList in GetListe)
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
        public static List<ViewModels.ResultatViewModel> GetListModulesEchoué(int idModuleInscription)  
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListModuleEchoué(idModuleInscription); 
            foreach (var itemList in GetListe)
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
        public static List<ViewModels.ResultatViewModel> GetListModulesRéussis(int idModuleInscription) 
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListModuleRéussis(idModuleInscription);   
            foreach (var itemList in GetListe)
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

    }
}
