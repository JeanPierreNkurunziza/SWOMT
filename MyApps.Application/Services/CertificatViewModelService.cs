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
        public static List<ViewModels.CertificatViewModel> GetCertificatsList(List<Infrastructure.DB.Certificat> listElement)
        {
            List<ViewModels.CertificatViewModel> Liste = new List<ViewModels.CertificatViewModel>();
           
            foreach (var itemList in listElement)
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

        /// <summary>
        /// méthose service pour afficher la liste de certificats
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetCertificats() 
        {
           
            var GetListe = CertificatService.GetAll();
           

            return GetCertificatsList(GetListe);
        }
        /// <summary>
        /// méthode qui va nour permettre d'afficher les certificats par participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns> une liste des certificats </returns>
        public static List<ViewModels.CertificatViewModel> GetCertificatsPerParticipant( int IdParticipant)
        {
           
            var GetListe = CertificatService.GetListPerParticipant(IdParticipant); 
            
             return GetCertificatsList(GetListe);
        }
    }
}
