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
            List<ViewModels.ParticipantViewModel> ParticipantsListe = new List<ViewModels.ParticipantViewModel>();
            var ListeParticipant = ParticipantService.GetAll();          
            foreach (var participant in ListeParticipant)
            {
                ViewModels.ParticipantViewModel vm = new ViewModels.ParticipantViewModel()
                {
                    IdParticipant = participant.IdParticipant,   
                    NomParticipant=participant.NomParticipant,
                    DateNaissance= participant.DateNaissance,
                    IdNational= participant.IdNational,   
                    TelParticipant= participant.TélParticipant,
                    EmailParticipant= participant.EmailParticipant,
                    SecteurParticipant= participant.SecteurParticipant,
                    DistrictParticipant= participant.DistrictParticipant,
                    DateEncodage= participant.DateEncodage,

                };
                ParticipantsListe.Add(vm);
            }

            return ParticipantsListe;
        }

        public static List<ParticipantViewModel> GetParticipantByMethodeSearch(string searchString) 
        {
            List<ViewModels.ParticipantViewModel> ParticipantsListe = new List<ViewModels.ParticipantViewModel>();
            var ListeParticipant = ParticipantService.SearchParticipantByName(searchString); 
            foreach (var participant in ListeParticipant)
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