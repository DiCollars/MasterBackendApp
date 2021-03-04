using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ServicesContracts
{
    public class AuthOptions
    {
        public const string ISSUER = "DiChat";

        public const string AUDIENCE = "User";

        const string KEY = "sdfghjJHGFghj13ghj12bgh4j45kh1";

        public const int LIFETIME = 10000;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
