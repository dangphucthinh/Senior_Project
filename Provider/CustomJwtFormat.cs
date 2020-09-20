using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
            //string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];

            //string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["as:AudienceSecret"].ToString();

            //var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            //var signingKey = new HmacSigningCredentials(keyByteArray);
            ////var signingKey = SigningCredentials();

            //var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(symmetricKeyAsBase64));
            //var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //var issued = data.Properties.IssuedUtc;

            //var expires = data.Properties.ExpiresUtc;

            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            //{
            //    new Claim("Username", data.Identity.Name),
            //});

            //var token = new JwtSecurityToken(_issuer, audienceId, claimsIdentity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingCredentials);

            //var handler = new JwtSecurityTokenHandler();

            //var jwt = handler.WriteToken(token);

            //return jwt;

            //    //Set issued at date
            //    DateTime issuedAt = DateTime.UtcNow;
            //    //set the time when it expires
            //    DateTime expires = DateTime.UtcNow.AddDays(7);

            //    //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            //    var tokenHandler = new JwtSecurityTokenHandler();

            //    //create a identity and add claims to the user which we want to log in
            //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            //    {
            //    new Claim(ClaimTypes.Name, data.Identity.Name)
            //});

            //    const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            //    var now = DateTime.UtcNow;
            //    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            //    var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //    //create the jwt
            //    var token =
            //        (JwtSecurityToken)
            //            tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:44335", audience: "http://localhost:44335",
            //                subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    return tokenString;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}