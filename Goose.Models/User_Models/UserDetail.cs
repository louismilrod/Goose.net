using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.User_Modles
{
    public class UserDetail
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }

    }
}
