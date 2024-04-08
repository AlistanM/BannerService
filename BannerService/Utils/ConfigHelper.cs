using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BannerService.Utils
{
    public class ConfigHelper
    {
        public static IConfiguration Configuration;

        public static string MsSqlConnectionString => Configuration["ConnectionStrings:MsSqlConnectionString"]!;
        public static string Issuer => Configuration["Authentication:Issuer"]!;
        public static string Audience => Configuration["Authentication:Audience"]!;
        public static string AuthenticationKeyRaw => Configuration["Authentication:Key"]!;
        public static string PasswordSalt => Configuration["Authentication:PasswordSalt"]!;

        public static SymmetricSecurityKey SecurityKey =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticationKeyRaw));

        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
