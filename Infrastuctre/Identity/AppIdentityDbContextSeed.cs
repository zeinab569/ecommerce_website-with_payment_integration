using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastuctre.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Zeinab",
                    Email = "zeinab@gmail.com",
                    UserName = "zeinab@gmail.com",
                    Address = new Address
                    {
                        FirstName = "zeinab",
                        LastName = "Elazab",
                        Street = "xxxx",
                        City = "Mansoura",
                        State = "EG",
                        ZipCode = "f3456",
                    }
                };
                await userManager.CreateAsync(user, "pass123");
            }
        }
    }
}
