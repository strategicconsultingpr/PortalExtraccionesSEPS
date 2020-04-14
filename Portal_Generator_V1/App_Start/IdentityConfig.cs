using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Portal_Generator_V1.Models;

namespace Portal_Generator_V1
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
            return Task.Factory.StartNew(() =>
            {
                sendMail(message);
            });

        }

        void sendMail(IdentityMessage message)
        {
            string text = string.Empty;
            string html = string.Empty;

            AlternateView imgview = AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html);

            LinkedResource lr = new LinkedResource("//vassmcaweb/wwwroot/LEY22/images/logo_1.jpg");
            lr.ContentId = "logo_1";
            lr.ContentType.MediaType = "image/jpg";
            imgview.LinkedResources.Add(lr);

            lr = new LinkedResource("//vassmcaweb/wwwroot/LEY22/images/logo_2.png");
            lr.ContentId = "logo_2";
            lr.ContentType.MediaType = "image/png";
            imgview.LinkedResources.Add(lr);

            lr = new LinkedResource("//vassmcaweb/wwwroot/LEY22/images/twitter-circle-colored.png");
            lr.ContentId = "twitter";
            lr.ContentType.MediaType = "image/png";
            imgview.LinkedResources.Add(lr);

            lr = new LinkedResource("//vassmcaweb/wwwroot/LEY22/images/facebook-circle-colored.png");
            lr.ContentId = "facebook";
            lr.ContentType.MediaType = "image/png";
            imgview.LinkedResources.Add(lr);

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("aortiz@assmca.pr.gov", "Portal Extracciones");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(imgview);
            msg.Body = lr.ContentId;


            SmtpClient smtpClient = new SmtpClient("vassmcaweb", Convert.ToInt32(25));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());//"aortiz@assmca.pr.gov", "Alexie@16");
            smtpClient.Credentials = credentials;
            //smtpClient.EnableSsl = true;
            smtpClient.Send(msg);

        }

        public class SmsService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your SMS service here to send a text message.
                return Task.FromResult(0);
            }
        }

        // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
                {
                    MessageFormat = "Your security code is {0}"
                });
                manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                manager.EmailService = new EmailService();
                manager.SmsService = new SmsService();
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }

        // Configure the application sign-in manager which is used in this application.
        public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
            {
                return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }
    }
}
