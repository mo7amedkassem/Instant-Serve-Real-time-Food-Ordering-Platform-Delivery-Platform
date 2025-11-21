using Job.Core.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Data.DBIdentity
{
    public class UserSeeding
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
          {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    UserName = "Mohamed",
                    Email = "Mohamed@gmail.com",
                    PhoneNumber = "1234567890"

                };
                await _userManager.CreateAsync(user, "Mohamed@123");
            }
        }
    }
}

