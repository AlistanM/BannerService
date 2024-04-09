using BannerService.Consts;
using BannerService.Dto.Banner;
using BannerService.Dto.User;
using BannerService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BannerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly TokenGenerationService _tokenGenerationService;
        private readonly BannerDtoService _bannerService;

        public ApiController(TokenGenerationService tokenGenerationService, BannerDtoService bannerService)
        {
            _tokenGenerationService = tokenGenerationService;
            _bannerService = bannerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<UserResultDto> Login([FromBody] UserDto user)
        {
            if (User.Identity.IsAuthenticated)
            {
                throw new Exception("Пользователь уже авторизован");
            }

            return await _tokenGenerationService.Login(user);
        }

        [HttpGet]
        [Route("user_banner")]
        [Authorize]
        public async Task<BannerDto> GetBanner([FromQuery] int tagId, [FromQuery] int featureId, [FromQuery] bool useLastRevision = false)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Пользовательне авторизован");
                await Response.CompleteAsync();
                return new BannerDto();
            }

            var banner = _bannerService.GetUserBanner(tagId, featureId);

            if (banner == null)
            {
                Response.StatusCode = 404;
                await Response.WriteAsync("Баннер для пользователя не найден");
                await Response.CompleteAsync();
                return new BannerDto();
            }

            if (!banner.IsActive.Value)
            {
                Response.StatusCode = 404;
                await Response.WriteAsync("Баннер отключен");
                await Response.CompleteAsync();
                return new BannerDto();
            }

            return banner;
        }

        [HttpGet]
        [Route("banner")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<BannerDto[]> GetBanners([FromQuery] int tagId, [FromQuery] int featureId, [FromQuery] int limit = 15, [FromQuery] int offset = 0)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Пользовательне авторизован");
                await Response.CompleteAsync();
                return Array.Empty<BannerDto>();
            }

            var banners = _bannerService.GetAdminBanners(tagId, featureId);

            return banners;
        }

        [HttpPost]
        [Route("banner")]
        [Authorize(Roles = Roles.Admin)]
        public async Task CreateBanner([FromBody] BannerDto banner)
        {
            await _bannerService.ToBanner(banner);
        }

        [HttpPatch]
        [Route("banner/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task UpdateBanner([FromBody] BannerDto banner, int id)
        {
            await _bannerService.UpdateBanner(banner, id);
        }

        [HttpDelete]
        [Route("banner/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task DeleteBanner(int id)
        {
            await _bannerService.DeleteBanner(id);
        }
    }
}
