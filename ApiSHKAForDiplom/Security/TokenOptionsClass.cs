using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiSHKAForDiplom.Security
{
    public class TokenOptionsClass
    {
        public const string ISSUER = "ServerAuthName";

        public const string AUDIENCE = "ClientAuthName";

        const string KEY = "EQGngiNEQINqiagQgqeigN";

        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
