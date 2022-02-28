using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models
{
    public class UserListItem
    {
        [Display(Name = "User Name")]
        public string UserId { get; set; }
        [Display(Name = "User Email")]
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
