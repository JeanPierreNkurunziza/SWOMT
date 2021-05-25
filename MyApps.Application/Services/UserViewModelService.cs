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
            var ListUserRoles = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles();
            foreach (var itemList in GetListe)
            {
                ViewModels.UserViewModel vm = new ViewModels.UserViewModel()
                {
                    IdUser = itemList.IdUser,
                    UserName = itemList.UserName,
                    MotDePasse = itemList.MotDePasse,
                    UserRole = itemList.UserRole,                  
                    
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Récuperer les roles des utilisateurs
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.UserViewModel> GetUsersRoles()
        {
            List<ViewModels.UserViewModel> Liste = new List<ViewModels.UserViewModel>();
            var GetListe = UserRolesServices.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.UserViewModel vm = new ViewModels.UserViewModel()
                {
                  
                    IdUserRole = itemList.IdUserRole,
                    UserRoleName = itemList.UserRoleName
                    

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Récuperer les utilisateurs selon leurs roles 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<ViewModels.UserViewModel> GetUsersByRoles(string roleName)
        {
            List<ViewModels.UserViewModel> Liste = new List<ViewModels.UserViewModel>();
            foreach (var item in GetUsers())
            {
                if (item.UserRole == roleName)
                {
                    Liste.Add(item);
                }
            }
            return Liste; 
        }
        /// <summary>
        /// Méthode pour faire une recherche selon le nom d'utilisateur 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<ViewModels.UserViewModel> SearchNameIntheList(string userName)
        {
            List<ViewModels.UserViewModel> Liste = new List<ViewModels.UserViewModel>();
            var items = from s in GetUsers()
                         select s; 

            if (!String.IsNullOrEmpty(userName))
                {
                  items = items.Where(s => s.UserName.ToUpper().Contains(userName.ToUpper()));
               
                }
            return items.ToList();
        }

    }
  
}
