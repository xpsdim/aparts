using System.Linq;
using Aparts.Models;
using Aparts.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aparts.Data
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            ApplicationDbContext db = (ApplicationDbContext)app.ApplicationServices.GetService(typeof(ApplicationDbContext));
            UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)app.ApplicationServices.GetService(typeof(UserManager<ApplicationUser>));
            IEmailSender emailSender = (IEmailSender) app.ApplicationServices.GetService(typeof (IEmailSender));

            //Roles creating
            var appRoles = new string[] { "Admin", "Seller", "Manager" };
            foreach (var role in appRoles)
            {
                var nextRole = db.Roles.SingleOrDefault(r => r.Name == role);
                if (nextRole == null)
                {
                    
                    nextRole = new IdentityRole(role);
                    nextRole.NormalizedName = role.ToUpper();
                    db.Roles.Add(nextRole);
                }
            }

            //Administrator creating
            var defAdminPassword = "Qwerty123#";
            var admin = db.Users.SingleOrDefault(u => u.Email == emailSender.SmtpSettings().AdminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser {UserName = emailSender.SmtpSettings().AdminEmail, Email = emailSender.SmtpSettings().AdminEmail};
                var result = userManager.CreateAsync(admin, defAdminPassword);

                if (result.Result.Succeeded)
                {
                    var code = userManager.GenerateEmailConfirmationTokenAsync(admin).Result;
                    var res = userManager.ConfirmEmailAsync(admin, code).Result;
                }
            }

            var res0 = userManager.AddToRolesAsync(admin, appRoles).Result;

            db.SaveChanges();

            emailSender.SendEmailAsync(emailSender.SmtpSettings().AdminEmail, "Your e-mail is admin account in new instance of Apart sytem.",
                        $"Use this e-mail as login and password -'{defAdminPassword}'. Please change this password as soon as possible.");
        }
    }
}
