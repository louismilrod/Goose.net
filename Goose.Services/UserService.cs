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


        public bool DeleteUser (string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == userId);

                ctx.Users.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
