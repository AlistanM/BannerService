using BannerService.Consts;
using BannerService.Data;
using BannerService.Data.Models;
using BannerService.Dto.User;
using BannerService.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BannerService.Services
{
    public class TokenGenerationService
    {
        private readonly DataContext _db;
        private readonly ClaimService _claimService;
        public TokenGenerationService(DataContext db, ClaimService claimService)
        {
            _db = db;
            _claimService = claimService;
        }

        public async Task<UserResultDto> Login(UserDto dto)
        {
            if (string.IsNullOrEmpty(dto.Login) || string.IsNullOrEmpty(dto.Password))
            {
                throw new Exception("Не введен логин или пароль");
            }

            var passwordHash = EncodingUtil.EncodePassword(dto.Password);

            var user = await _db.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    Login = x.Login,
                    Password = x.Password,
                    Role = x.Role
                })
                .FirstOrDefaultAsync(x => x.Login == dto.Login && x.Password == passwordHash);

            if (user == null)
            {
                throw new Exception("Неверный логин или пароль");
            }

            var claims = CreateClaims(user);
            _claimService.AddClaims(claims);

            var token = new JwtSecurityToken(
                issuer: ConfigHelper.Issuer,
                audience: ConfigHelper.Audience,
                signingCredentials: new SigningCredentials(ConfigHelper.SecurityKey, SecurityAlgorithms.HmacSha256),
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60));

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserResultDto
            {
                Token = tokenString
            };

        }

        public Claim[] CreateClaims(User user)
        {
            return new Claim[]
            {
                new(Claims.UserId, user.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
        }
    }
}
