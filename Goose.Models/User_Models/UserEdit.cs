using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.User_Models
{
    public class UserEdit
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }

    }
}
