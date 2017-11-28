using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuessAnimalGame.Startup))]
namespace GuessAnimalGame
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
