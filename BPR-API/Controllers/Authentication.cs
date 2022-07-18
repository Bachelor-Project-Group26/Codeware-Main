using BPR_API.APIModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BPR_API.Controllers
{
    public class Authentication
    {
        public static string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("a1b2c3d4e5f6"));
            //_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new Byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string password, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return System.Text.Encoding.UTF8.GetString(hash, 0, hash.Length);
        }

        public static bool VerifyToken(string token)
        {
            return true;
            //bool isVerified = true;
            //
            //var splitToken = token.Split("=");
            //if (splitToken.Length > 2) isVerified = false;
            //
            //user = null;
            //User tempUser = null;
            //try
            //{
            //    var id = Int32.Parse(splitToken[0]);
            //    tempUser = _dbContext.UserPasswords
            //        .First(a => a.Id == id);
            //}
            //catch (InvalidOperationException)
            //{
            //    isVerified = false;
            //}
            //
            //if (isVerified)
            //{
            //    user = tempUser;
            //    if (splitToken[1] != user.Token)
            //    {
            //        isVerified = false;
            //        user = null;
            //    }
            //}
        }
    }
}
