using KoiFish_Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(KoiFishDbContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var rootAdminRoleId = Guid.NewGuid();

            if (!context.Roles.Any(r => r.Name.Equals("Admin")))
            {
                await context.Roles.AddAsync(new AppRole()
                {
                    Id = rootAdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Administrator"
                });
                await context.SaveChangesAsync();

                //add user
                var userId = Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    FullName = "asd",
                    Email = "asd@gmail.com",
                    NormalizedEmail = "ASD@GMAIL.COM",
                    UserName = "asd",
                    NormalizedUserName = "ASD",
                    Status = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    CreatedAt = DateTime.Now,
                    EmailConfirmed = true,
                    BirthYear = 2000
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "123As@");
                await context.Users.AddAsync(user);

                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRoleId,
                    UserId = userId,
                });
                await context.SaveChangesAsync();
            }
              if (!context.Users.Any(u => u.UserName.Equals("admin2")))
            {
                var adminId = Guid.NewGuid();
                var adminUser = new AppUser()
                {
                    Id = adminId,
                    FullName = "Admin Two",
                    Email = "admin2@gmail.com",
                    NormalizedEmail = "ADMIN2@GMAIL.COM",
                    UserName = "admin2",
                    NormalizedUserName = "ADMIN2",
                    Status = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    CreatedAt = DateTime.Now,
                    EmailConfirmed = true,
                    BirthYear = 1990
                };
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "123As@");
                await context.Users.AddAsync(adminUser);

                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRoleId,
                    UserId = adminId,
                });
        }  var customers = new[]
            {
                new { FullName = "Nguyen Van A", Email = "nguyenvana@gmail.com", UserName = "nguyenvana", BirthYear = 1995 },
                new { FullName = "Tran Thi B", Email = "tranthib@gmail.com", UserName = "tranthib", BirthYear = 1996 },
                new { FullName = "Le Van C", Email = "levanc@gmail.com", UserName = "levanc", BirthYear = 1997 },
                new { FullName = "Pham Thi D", Email = "phamthid@gmail.com", UserName = "phamthid", BirthYear = 1998 },
                new { FullName = "Hoang Van E", Email = "hoangvane@gmail.com", UserName = "hoangvane", BirthYear = 1999 }
            };

            foreach (var customer in customers)
            {
                if (!context.Users.Any(u => u.UserName.Equals(customer.UserName)))
                {
                    var customerId = Guid.NewGuid();
                     var customerRoleId = Guid.NewGuid();
                    var customerUser = new AppUser()
                    {
                        Id = customerId,
                        FullName = customer.FullName,
                        Email = customer.Email,
                        NormalizedEmail = customer.Email.ToUpper(),
                        UserName = customer.UserName,
                        NormalizedUserName = customer.UserName.ToUpper(),
                        Status = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        LockoutEnabled = false,
                        CreatedAt = DateTime.Now,
                        EmailConfirmed = true,
                        BirthYear = customer.BirthYear
                    };
                    customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, "123As@");
                    await context.Users.AddAsync(customerUser);

                    await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                    {
                        RoleId = customerRoleId,
                        UserId = customerId,
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}