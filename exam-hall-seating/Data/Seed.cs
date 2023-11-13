using exam_hall_seating.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exam_hall_seating.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!await roleManager.RoleExistsAsync(AppRole.Admin))
                    await roleManager.CreateAsync(new AppRole { Name = AppRole.Admin });
                if (!await roleManager.RoleExistsAsync(AppRole.Instructor))
                    await roleManager.CreateAsync(new AppRole { Name = AppRole.Instructor });

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var newAdminUser = new AppUser()
                {
                    UserName = "adminuser",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "5004003020",
                    PhoneNumberConfirmed = true,
                    FirstName = "adminfirst",
                    LastName = "adminlast"
                };
                var result = await userManager.CreateAsync(newAdminUser, "Admin12345?");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, AppRole.Admin);
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        Console.WriteLine($"Hata Kodu: {error.Code}");
                        Console.WriteLine($"Hata Açıklaması: {error.Description}");
                        Console.WriteLine();
                    }
                }

                var newAppUser = new AppUser()
                {
                    UserName = "ogretmentest",
                    Email = "ogretmen@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "5005005050",
                    PhoneNumberConfirmed = true,
                    FirstName = "ÖğretmenAd",
                    LastName = "ÖğretmenSoyad",
                    DepartmentId = 1
                };


                var result2 = await userManager.CreateAsync(newAppUser, "Ogretmen12345?");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAppUser, AppRole.Instructor);
                }
                else
                {
                    foreach (var error in result2.Errors)
                    {
                        Console.WriteLine($"Hata Kodu: {error.Code}");
                        Console.WriteLine($"Hata Açıklaması: {error.Description}");
                        Console.WriteLine();
                    }
                }
                

            }
        }
    }
}
