using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Models.User_Models
{
    public class UserEdit
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }

    }
}
