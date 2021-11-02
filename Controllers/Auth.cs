using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using pediagnoswebapi.Models;
using pediagnoswebapi.Models.DB;

namespace pediagnoswebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Auth : Controller
    {
        private readonly PediagnosDBContext _context;
        IConfiguration _configuration;

        public Auth(PediagnosDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public ApiResponse GetToken([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.KullaniciAdi == request.UserName && u.Sifre == request.Password);
                if (user == null)
                {
                    return new ApiResponse { Code = 201, Message = "Kullanıcı mevcut değil" };
                }
                var claims = new[]{
                    new Claim(JwtRegisteredClaimNames.Sub,request.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigninKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenHandler = new JwtSecurityToken(
                    issuer: _configuration["Issuer"],
                    audience: _configuration["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: credentials
                );
                IdentityModelEventSource.ShowPII = true;
                var token = new JwtSecurityTokenHandler().WriteToken(tokenHandler);

                user.Token = token;
                _context.SaveChanges();
                return new ApiResponse { Code = 200, Message = "Token üretildi", Set = new { token = token, rol = user.Rol } };
            }
            return new ApiResponse { Code = 202, Message = "Model validate değil, kullanıcı adı şifre yazmalasınız" };
        }
        [HttpPost("ValidateToken")]
        public ApiResponse ValidateToken([FromBody] ValidateTokenModel request)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigninKey"]));
            var myIssuer = _configuration["Issuer"];
            var myAudience = _configuration["Audience"];
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch (System.Exception)
            {

                return new ApiResponse { Code = 400, Message = "Token doğru değil" };
            }
            var user = _context.Users.FirstOrDefault(u => u.Token == request.Token);
            return new ApiResponse { Code = 200, Message = "Token doğru", Set = user.Rol };
        }

    }
}