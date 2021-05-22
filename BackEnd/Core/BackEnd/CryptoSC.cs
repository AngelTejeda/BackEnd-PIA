using Core.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.BackEnd
{
    public class CryptoSC
    {
        // Compara una contraseña encriptada con una contraseña en texto plano y devuelve true si son iguales
        // y false en caso contrario.
        public static bool ComparePasswords(string dbHashedPassword, string plainPassword, string salt)
        {
            string hashedPassword = GetHashedPassword(plainPassword, salt);

            return hashedPassword == dbHashedPassword;
        }

        // Genera el Hash de la contraseña que se recibe como parámetro, agregando el salt indicado.
        // Utiliza el algoritmo PBKDF2 con 5000 iteraciones.
        public static string GetHashedPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);  //20 bytes - 160 bits

            byte[] pbkdf2Bytes = new Rfc2898DeriveBytes(password, saltBytes, iterations: 5000).GetBytes(20);
            string hashedPassword = Convert.ToBase64String(pbkdf2Bytes);

            return hashedPassword;
        }

        // Genera un salt de 128 bits (16 bytes) y lo devuelve convertido a un string base 64.
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            string salt = Convert.ToBase64String(saltBytes);

            return salt;
        }

        // Genera un Token JWT con los datos del usuario que recibe.
        public static string GenerateToken(User dbUser)
        {
            JwtParamethers jwt = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwt.GetSecretKey()));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, dbUser.Username),
                new Claim(JwtRegisteredClaimNames.Email, dbUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            JwtSecurityToken securityToken = new(
                issuer: jwt.GetIssuer(),
                audience: jwt.GetIssuer(),
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
