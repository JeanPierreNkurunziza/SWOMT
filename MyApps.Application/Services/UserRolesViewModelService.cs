using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class UserRolesViewModelService
    {
        public static List<ViewModels.UserRolesViewModel> GetUsersRoles()
        {
            List<ViewModels.UserRolesViewModel> Liste = new List<ViewModels.UserRolesViewModel>();
            var GetListe = UserRolesServices.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.UserRolesViewModel vm = new ViewModels.UserRolesViewModel()
                {
                    IdUserRole = itemList.IdUserRole,
                    UserRoleName = itemList.UserRoleName,

                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
