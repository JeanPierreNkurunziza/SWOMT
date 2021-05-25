using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace MyApps.Application.ViewModels
{
    public class UserViewModel
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string MotDePasse { get; set; }
        public string UserRole { get; set; }
        public UserRolesViewModel ListUserRole { get; set; }         
        public int IdUserRole { get; set; }
        public string UserRoleName { get; set; }
    }
   
}
