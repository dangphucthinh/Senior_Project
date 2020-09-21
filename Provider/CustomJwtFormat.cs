using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Thinktecture.IdentityModel.Tokens;


namespace Doctor_Appointment.Provider
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];

            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["as:AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;



            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("Username", data.Identity.Name),
                new Claim("ID", data.Identity.GetUserId())
            });;

            ApplicationDbContext db = new ApplicationDbContext();
            //var lstRole = db.U.Where(e => e.Name == data.Identity.Name);
            SqlParameter param = new SqlParameter() { ParameterName = "username", SqlDbType = System.Data.SqlDbType.NVarChar, Value = data.Identity.Name };
            //var tmp = db.Database.SqlQuery<object>("EXEC GetRoles @username", param);
            List<string> lstRoles = db.Database.SqlQuery<string>("EXEC GetRoles @username", param).ToList();
            foreach (var item in lstRoles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, item));
            }
            var token = new JwtSecurityToken(_issuer, audienceId, claimsIdentity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
            
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}