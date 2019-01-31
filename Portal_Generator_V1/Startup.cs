using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Portal_Generator_V1.Models;

[assembly: OwinStartupAttribute(typeof(Portal_Generator_V1.Startup))]
namespace Portal_Generator_V1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUsersAndRoles();
        }
        public void CreateUsersAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@assmca.pr.gov";
                user.Email = "admin@assmca.pr.gov";
                string pwd = "Admin@2018";
                var newuser = userManager.Create(user, pwd);
                if(newuser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole("Manager");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("SepsR"))
            {
                var role = new IdentityRole("SepsR");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("TedsR"))
            {
                var role = new IdentityRole("TedsR");
                roleManager.Create(role);
            }

        }
    }
}
