using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Products.backend.Startup))]
namespace Products.backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
