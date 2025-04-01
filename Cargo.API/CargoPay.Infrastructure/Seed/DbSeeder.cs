using CargoPay.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CargoPay.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(CargoPayDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "admin"
                };

                var hasher = new PasswordHasher<User>();
                user.PasswordHash = hasher.HashPassword(user, "admin");

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
