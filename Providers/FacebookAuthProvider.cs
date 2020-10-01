using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;


namespace Doctor_Appointment.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new System.Security.Claims.Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }
    }
}