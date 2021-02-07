using Microsoft.Owin;
using MisDatos2;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(CursoProvimad1.Startup))]
namespace CursoProvimad1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUsers();
        }

        protected void CreateUsers()
        {
            using (MisDatos2.CursoEntities db = new MisDatos2.CursoEntities())
            {
                User admin = db.Users.FirstOrDefault(d => d.UserName == "admin");
                if (admin == null)
                {
                    admin = new MisDatos2.User()
                    {
                        UserName = "admin",
                        Password = "admin",
                        Role = 1
                    };
                    db.Users.Add(admin);
                    db.SaveChanges();
                }
            }
        }
    }
}
