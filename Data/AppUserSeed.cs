using Microsoft.AspNetCore.Identity;
using webshop_2.Data;

namespace webshop_2.Data
{
    public class AppUserSeed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //add roles to database if they don't exist
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                //seed admin user
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var admin = new AppUser()
                    {
                        UserName = "admin@gmail.com",
                        Name = "admin@gmail.com",
                        Email = adminUserEmail,
                        StreetAddress = "schoolstraat 1",
                        City = "Gent",
                        PostalCode = "9040",
                        Country = "Belgium",
                        CardNumber = "4111111111111111"
                    };
                    await userManager.CreateAsync(admin, "admin123");
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }

                //seed regular user
                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "user@gmail.com",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        StreetAddress = "schoolstraat 1",
                        Name = "user@gmail.com",
                        City = "Gent",
                        PostalCode = "9040",
                        Country = "Belgium",
                        CardNumber = "4111111111111111"
                    };
                    await userManager.CreateAsync(newAppUser, "user123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
