using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Pysco68.Owin.Logging.NLogAdapter;
using Thinktecture.IdentityServer.AccessTokenValidation;
using Thinktecture.IdentityServer.Core.Configuration;

[assembly: OwinStartup(typeof(SSODemo.Startup))]

namespace SSODemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:52401",//Your url here
                RequiredScopes = new[] {"api"}
            });

            app.UseWebApi(config);
            var factory = InMemoryFactory.Create(Config.GetUsers().ToList(), Config.GetClients(), Config.GetScopes());
            app.UseIdentityServer(new IdentityServerOptions
            {
                IssuerUri = "urn:identity",
                Factory = factory,
                RequireSsl = false, //DO NOT DO THIS IN PRODUCTION
                LoggingOptions = new LoggingOptions
                {
                 EnableWebApiDiagnostics   = true,
                 WebApiDiagnosticsIsVerbose = true
                },
                SigningCertificate = LoadCertificate()
               
            });
            app.UseNLog();
        }
        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
