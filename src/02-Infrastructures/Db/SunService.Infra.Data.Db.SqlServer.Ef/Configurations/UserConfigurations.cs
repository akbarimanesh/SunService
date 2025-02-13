

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.UserS.Entities;

namespace SunService.Infra.Data.Db.SqlServer.Ef.Configurations
{
    public class UserConfigurations
    {
        public static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            //SeedUsers
            var users = new List<User>
        {
            new User()
            {
                Id = 1,
                UserName = "Admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                Mobile = "09196043564",
                SecurityStamp = Guid.NewGuid().ToString(),
                CityId = 1,
                RoleId = 1
            }
        };

            foreach (var user in users)
            {
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, "123456");
                builder.Entity<User>().HasData(user);
            }

            // Seed Roles
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int>() { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole<int>() { Id = 3, Name = "Expert", NormalizedName = "EXPERT" }
            );

            //Seed Role To Users
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
            );
        }
    }
}
