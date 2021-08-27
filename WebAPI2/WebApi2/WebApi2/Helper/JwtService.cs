
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi2.App_Data;
using WebApi2.Models;

namespace Currency.Helper
{
    public class JwtService
    {
        
        /// <summary>
        /// Generate  token JWT
        /// </summary>
        /// <param name="customUser">User to Encode</param>
        /// <returns></returns>
        public  static async Task<string> GenerateToken(CustomUser customUser)
        {
            try
            {
                
                var secretKey = ConfigurationManager.AppSettings["JWT_SecretKeyToken"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_IssuerToken"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AudienceToken"];
                var expireTime = ConfigurationManager.AppSettings["JWT_ExpireMinutes"];


                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Create a List of Claims, Keep claims name short    
                var permClaims = new List<Claim>();
                permClaims.Add(new Claim("UserId", customUser.Use_Id.ToString()));
               

                double minutes = 25;
                bool isDouble = double.TryParse( expireTime, out minutes);
                var token = new JwtSecurityToken(issuerToken, //Issure    
                               audienceToken,  //Audience    
                               permClaims,                            
                               expires: DateTime.Now.AddMinutes(minutes),
                               signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt_token;
            }
            catch(Exception)
            {
                throw;
            }
        }

        



    }
}
