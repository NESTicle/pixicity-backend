using Microsoft.Extensions.Options;
using Pixicity.Domain.AppSettings;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Pixicity.Service.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly KeysAppSettingsViewModel _keys;

        public JwtService(IOptions<KeysAppSettingsViewModel> keys)
        {
            _keys = keys.Value;
        }

        public byte[] GetHashKeyJwt()
        {
            return Encoding.ASCII.GetBytes(_keys.JWT);
        }

        public string GetTokenFromJWT(string jwt)
        {
            return jwt.Replace("Bearer ", "");
        }

        public string GetUniqueName(string jwt)
        {
            try
            {
                if (string.IsNullOrEmpty(jwt))
                    throw new Exception("Por favor proporcione un token jwt");

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(GetTokenFromJWT(jwt));

                if (jwtToken == null)
                    throw new Exception("No ha sido posible leer el token jwt");

                return jwtToken.Payload["unique_name"].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
