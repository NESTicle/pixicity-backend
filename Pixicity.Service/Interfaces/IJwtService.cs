using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IJwtService
    {
        byte[] GetHashKeyJwt();

        string GetTokenFromJWT(string jwt);

        string GetUniqueName(string jwt);
    }
}
