using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class UserViewModelService
    {
        /// <summary>
        /// méthode pour récuperer les utilisateurs et leur roles 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.UserViewModel> GetUsers()
        {
            List<ViewModels.UserViewModel> Liste = new List<ViewModels.UserViewModel>();
            var GetListe = UserService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.UserViewModel vm = new ViewModels.UserViewModel()
                {
                    IdUser = itemList.IdUser,
                    UserName = itemList.UserName,
                    MotDePasse = itemList.MotDePasse,
                    UserRole = itemList.UserRole 
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
