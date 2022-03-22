using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcETicaretSenaErcihanCelik.Startup))]
namespace MvcETicaretSenaErcihanCelik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
