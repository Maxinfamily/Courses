using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CursoProvimad1.Models;
using MisDatos2;

namespace CursoProvimad1
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte su servicio de correo electrónico aquí para enviar correo electrónico.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte el servicio SMS aquí para enviar un mensaje de texto.
            return Task.FromResult(0);
        }
    }



    public class MyUser : User, IUser<string>
    {
        public new string Id
        {
            get
            {
                return base.Id.ToString();
            }
        }

        public string PasswordHash
        {
            get { return Password; }
        }

        public MyUser(User u)
        {
            UserName = u.UserName;
            base.Id = u.Id;
            Password = u.Password;
            Role = u.Role;
        }

        public MyUser()
        {

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MyUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class MyUserStore : IUserStore<MyUser>, IUserPasswordStore<MyUser>
    {
        protected CursoEntities db = new CursoEntities();
        #region IUserStore
        public Task CreateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<MyUser> FindByIdAsync(string userId)
        {
            User _u = await db.Users.FirstOrDefaultAsync(d => d.Id.ToString() == userId);
            if (_u != null)
            {
                return new MyUser(_u);
            }

            return null;
        }

        public async Task<MyUser> FindByNameAsync(string userName)
        {
            User _u =await db.Users.FirstOrDefaultAsync(d => d.UserName == userName);
            if (_u != null)
            {
                return new MyUser(_u);
            }

            return null;
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(MyUser user)
        {
            User u = db.Users.Find(int.Parse(user.Id));
            return Task.Factory.StartNew<string>(() => u.Password);
        }

        public Task<bool> HasPasswordAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MyUser user)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

    // Configure el administrador de usuarios de aplicación que se usa en esta aplicación. UserManager se define en ASP.NET Identity y se usa en la aplicación.
    public class ApplicationUserManager : UserManager<MyUser>
    {
        public ApplicationUserManager(IUserStore<MyUser> store)
            : base(store)
        {
        }

        public override Task<bool> CheckPasswordAsync(MyUser user, string password)
        {
            return Task.Run(() => (user.PasswordHash == password));
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
           
            MyUserStore _usr = new MyUserStore();

            var manager = new ApplicationUserManager(_usr);

            return manager;
        }

       
         
    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Configure la lógica de validación de nombres de usuario
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };

    //        // Configure la lógica de validación de contraseñas
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };

    //        // Configurar valores predeterminados para bloqueo de usuario
    //        manager.UserLockoutEnabledByDefault = true;
    //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        // Registre proveedores de autenticación en dos fases. Esta aplicación usa los pasos Teléfono y Correo electrónico para recibir un código para comprobar el usuario
    //        // Puede escribir su propio proveedor y conectarlo aquí.
    //        manager.RegisterTwoFactorProvider("Código telefónico", new PhoneNumberTokenProvider<ApplicationUser>
    //        {
    //            MessageFormat = "Su código de seguridad es {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("Código de correo electrónico", new EmailTokenProvider<ApplicationUser>
    //        {
    //            Subject = "Código de seguridad",
    //            BodyFormat = "Su código de seguridad es {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = 
    //                new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}

    // Configure el administrador de inicios de sesión que se usa en esta aplicación.
    public class ApplicationSignInManager : SignInManager<MyUser, string>
    {

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(MyUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
        public override async Task SignInAsync(MyUser user, bool isPersistent, bool rememberBrowser)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //Using the UserMaster data, set our custom claim types
            /*
            // http://stackoverflow.com/questions/32880269/how-to-do-session-management-in-aspnet-identity
            identity.AddClaim(new Claim(BSClaimTypes.Profile, user.Profile.ToString()));
            // Add custom user claims here => this.OrganizationId is a value stored in database against the user identity.AddClaim(new Claim(ACCOUNT_ID, user.IdAccount.ToString()));
            // Logo de la cuenta
            identity.AddClaim(new Claim(LOGO, user.LogoCustom)); */
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private async Task<SignInStatus> SignInOrTwoFactor(MyUser user, bool isPersistent)
        {
            var id = Convert.ToString(user.Id);
            if (UserManager.SupportsUserTwoFactor
            && await UserManager.GetTwoFactorEnabledAsync(user.Id)
            //.WithCurrentCulture()
            && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id)
            //.WithCurrentCulture()
            ).Count > 0
            && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id)
            //.WithCurrentCulture()
            )
            {
                var identity = new ClaimsIdentity(
                DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresVerification;
            }
            await SignInAsync(user, isPersistent, false); //.WithCurrentCulture();
            return SignInStatus.Success;
        }
        public override async Task<SignInStatus> PasswordSignInAsync(
        string userName,
        string password,
        bool isPersistent,
        bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName); //.WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (UserManager.SupportsUserPassword
            && await UserManager.CheckPasswordAsync(user, password))
            //.WithCurrentCulture())
            {
                return await SignInOrTwoFactor(user, isPersistent); //.WithCurrentCulture();
            }
            return SignInStatus.Failure;
        }
    }
}
