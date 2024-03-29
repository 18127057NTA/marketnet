using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user = new AppUser{
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    PhoneNumber = "0918367172",
                    UserName = "bob@test.com",
                    Address = new Address {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "115 Ben Van Don",
                        Ward = "4",
                        District = "4",
                        City = "Hochiminh",
                        Province = "HCM",
                        ZipCode = "70000"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}