using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    /// <summary>
    /// Service view model
    /// </summary>
   public class CertificatViewModelService
    {
        /// <summary>
        /// méthose service pour afficher la liste de certificats
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetCertificats() 
        {
            List<ViewModels.CertificatViewModel> Liste = new List<ViewModels.CertificatViewModel>();
            var GetListe = CertificatService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.CertificatViewModel vm = new ViewModels.CertificatViewModel()
                {
                    IdCertificat = itemList.IdCertificat,
                    IdParticipant = itemList.IdParticipant,
                    NomParticipant=CertificatService.GetNomParticipant(itemList.IdParticipant), 
                    DateDelivrance=itemList.DateDelivrance,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// méthode qui va nour permettre d'afficher les certificats par participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns> une liste des certificats </returns>
        public static List<ViewModels.CertificatViewModel> GetCertificatsPerParticipant( int IdParticipant)
        {
            List<ViewModels.CertificatViewModel> Liste = new List<ViewModels.CertificatViewModel>();
            var GetListe = CertificatService.GetListPerParticipant(IdParticipant); 
            foreach (var itemList in GetListe)
            {
                ViewModels.CertificatViewModel vm = new ViewModels.CertificatViewModel()
                {
                    IdCertificat = itemList.IdCertificat,
                    IdParticipant = itemList.IdParticipant,
                    NomParticipant = CertificatService.GetNomParticipant(itemList.IdParticipant),
                    DateDelivrance = itemList.DateDelivrance,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
