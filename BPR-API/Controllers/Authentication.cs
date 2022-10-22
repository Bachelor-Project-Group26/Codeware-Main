using BPR_API.APIModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BPR_API.Controllers
{
    /// <summary>
    /// This is where all the methods related to the authentication are implemented.
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Creates a JSON Web Token.
        /// </summary>
        /// <param name="userDTO">Carries data related to the user between the client and the API.</param>
        /// <param name="configuration">Contains values written on the appsettings.json file.</param>
        /// <returns>Returns the JSON Web Token</returns>
        public static string CreateToken(UserDTO userDTO, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDTO.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generates a string to salt the password before it is hashed.
        /// </summary>
        /// <param name="size">The size of the string.</param>
        /// <returns>Returns the string that will be used for the salting process.</returns>
        public static string GenerateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new Byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Generates a hashed passoword using the password and the salt.
        /// </summary>
        /// <param name="password">The password that is going to be hashed and salted.</param>
        /// <param name="salt">The string to salt the password.</param>
        /// <returns>The hashed and salted password.</returns>
        public static string GenerateHash(string password, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        }
    }
}
