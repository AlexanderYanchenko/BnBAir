using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BnBAir.API.AuthenticationOptions
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "AuthClient";
        public const string KEY = "topsecretkey_forn1x";
        public const int LIFETIME = 300;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}