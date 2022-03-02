using Goose.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goose.Services
{
    public class UserService
    {
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Users.ToList();
            }
        }      

    }
}
