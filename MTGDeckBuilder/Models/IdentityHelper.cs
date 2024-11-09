using Microsoft.AspNetCore.Identity;
using MTGDeckBuilder.Data;
#nullable disable
namespace MTGDeckBuilder.Models
{
    public static class IdentityHelper
    {
        public const string Admin = "Admin";
        public const string GeneralUser = "GeneralUser";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach(string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if(!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateAdmin(IServiceProvider provider)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = provider.GetRequiredService<ApplicationDbContext>();

            string adminEmail = "Admin@gmail.com";

            // Check if the admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // Temporary admin role, would want to keep this information hidden
                // when site would be live
                var admin = new IdentityUser()
                {
                    Email = "Admin@gmail.com",
                    UserName = "Admin"                    
                };

                await userManager.CreateAsync(admin, "Abc123!");
                await userManager.AddToRoleAsync(admin, Admin);

                // Create the UserInventory for the admin
                var userInventory = new UserInventory {User = admin}; 
                context.UserInventories.Add(userInventory); 
                await context.SaveChangesAsync();
            }
        }
    }
}
