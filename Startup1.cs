using System;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.Owin;
using Doctor_Appointment.Models;
using Doctor_Appointment.Provider;
using System.Web.Http;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Configuration;
using Microsoft.Owin.Security.Jwt;
using System.Net.Http.Formatting;
using Microsoft.Owin.Security;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.Facebook;
using Doctor_Appointment.Providers;

[assembly: OwinStartup(typeof(Doctor_Appointment.Startup1))]

namespace Doctor_Appointment
{
    public partial class Startup1
    {
        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthenticationOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            
            HttpConfiguration httpConfig = new HttpConfiguration();
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);
        }
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Auth/Login"),
                AuthorizeEndpointPath = new PathString("/api/Auth/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(7),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("https://localhost:44355/")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = "624214714911167",
                AppSecret = "c6c727e3400693316daea3cb6a464a91",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthenticationOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = "https://localhost:44355/";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"].ToString());

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
               new JwtBearerAuthenticationOptions
               {
                   AuthenticationMode = AuthenticationMode.Active,
                   AllowedAudiences = new[] { audienceId },
                   IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                   {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                   }
               });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

    }
}
