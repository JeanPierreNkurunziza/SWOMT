using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class CertificatViewModelService
    {
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
    }
}
