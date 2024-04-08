using BannerService.Data;
using BannerService.Data.Models;
using BannerService.Dto.Banner;
using System.Data.Entity;

namespace BannerService.Services
{
    public class BannerDtoService
    {
        private readonly DataContext _db;
        public BannerDtoService(TokenGenerationService tokenGenerationService, DataContext db)
        {
            _db = db;
        }

        public BannerDto? GetUserBanner(int tag_id, int feature_id)
        {
            var banner = _db.Banners.Where(banner => banner.Tags.Any(x => x.Id == tag_id) && banner.Features.Any(x => x.Id == feature_id)).ToList().FirstOrDefault();
            if (banner == null)
            {
                return null;
            }
            return new BannerDto { BannerId = banner.Id, Content = banner.Content, IsActive = banner.IsActive };
        }

    }
}
