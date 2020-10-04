
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Provider
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.AdditionalResponseParameters.Add("status", 1);
            context.AdditionalResponseParameters.Add("message", "success");
            return base.TokenEndpoint(context);
        }
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        class ErrorMessage
        {
            public int status { get; set; }
            public string message { get; set; } 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                //context.SetError("invalid_grant", "The user name or password is incorrect.");
                //context.Response.Set<int>("status", 0);
                //context.Response.Set<string>("message", "The user name or password is incorrect.");
                var mes = new ErrorMessage() { status = 0, message = "The user name or password is incorrect." };
                string jsonString = JsonConvert.SerializeObject(mes);

                // This is just a work around to overcome an unknown internal bug. 
                // In future releases of Owin, you may remove this.
                //context.SetError(jsonString);

                //context.Response.Body.
                context.Response.StatusCode = 400;
                context.Response.Write(jsonString);
                //context.Ticket.
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            //oAuthIdentity.AddClaim(ExtendedClaimsProvider.GetClaims(user))
           
            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }
    }
}