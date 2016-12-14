using donutrun.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(donutrun.Startup))]
namespace donutrun
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup I am creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                //    // first we create Admin role   
                //    var role = new IdentityRole()
                //    {
                //        Name = "Admin"
                //    };
                //    roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "admin@test.com"
                };
                string userPWD = "P@$$word123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
                else
                {
                    throw new Exception(string.Join(",", chkUser.Errors));
                }
            }

            // creating Creating Driver role    
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new IdentityRole()
                {
                    Name = "Driver"
                };
                roleManager.Create(role);

            }

            // creating Creating Customer role    
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole()
                {
                    Name = "Customer"
                };
                roleManager.Create(role);

            }

            context.SaveChanges();
        }
    }
}
