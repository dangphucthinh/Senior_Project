﻿using Doctor_Appointment.App_Start;
using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
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
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            //oAuthIdentity.AddClaim(ExtendedClaimsProvider.GetClaims(user))
           
            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(property.Key, property.Value);
        //    }

        //    return Task.FromResult<object>(null);
        //}


        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    // Resource owner password credentials does not provide a client ID.
        //    if (context.ClientId == null)
        //    {
        //        context.Validated();
        //    }

        //    return Task.FromResult<object>(null);
        //}

        //public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    if (context.ClientId == _publicClientId)
        //    {
        //        Uri expectedRootUri = new Uri(context.Request.Uri, "/");

        //        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        //        {
        //            context.Validated();
        //        }
        //    }

        //    return Task.FromResult<object>(null);
        //}

        //public static AuthenticationProperties CreateProperties(string userName)
        //{
        //    IDictionary<string, string> data = new Dictionary<string, string>
        //    {
        //        { "userName", userName }
        //    };
        //    return new AuthenticationProperties(data);
        //}
    }
}