using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApps.Application.ViewModels;
using MyApps.Domain.Service;

namespace MyApps.Application.Services
{
   
    public class ParticipantsViewModelServices
    {
      
        public static List<ParticipantViewModel> GetParticipants()
        {
           
            var ListeParticipant = ParticipantService.GetAll();
            return GetParticipantsList(ListeParticipant);
        }
        /// <summary>
        /// afficher la liste selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ParticipantViewModel> GetParticipantByMethodeSearch(string searchString) 
        {
           
            var ListeParticipant = ParticipantService.SearchParticipantByName(searchString); 
           

            return GetParticipantsList(ListeParticipant);
        }
        public static List<ParticipantViewModel> GetParticipantsList(List<Infrastructure.DB.Participant> listElement)
        {
            List<ViewModels.ParticipantViewModel> ParticipantsListe = new List<ViewModels.ParticipantViewModel>();
          
            foreach (var participant in listElement)
            {
                ViewModels.ParticipantViewModel vm = new ViewModels.ParticipantViewModel()
                {
                    IdParticipant = participant.IdParticipant,
                    NomParticipant = participant.NomParticipant,
                    DateNaissance = participant.DateNaissance,
                    IdNational = participant.IdNational,
                    TelParticipant = participant.TélParticipant,
                    EmailParticipant = participant.EmailParticipant,
                    SecteurParticipant = participant.SecteurParticipant,
                    DistrictParticipant = participant.DistrictParticipant,
                    DateEncodage = participant.DateEncodage,

                };
                ParticipantsListe.Add(vm);
            }

            return ParticipantsListe;
        }

    }
}