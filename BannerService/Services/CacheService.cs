using BannerService.Cache;
using BannerService.Data.Models;
using BannerService.Dto.Banner;

namespace BannerService.Services
{
    public class CacheService
    {
        private readonly BannerDtoService _bannerService;
        private static GetCache cache = new GetCache();
        public static UpdateCache UCache = new UpdateCache();

        public CacheService(BannerDtoService bannerService)
        {
            _bannerService = bannerService;
        }

        public BannerDto? GetUserBanner(int tag_id, int feature_id)
        {

            foreach (Banner banner in cache.BannerCache.Values)
            {
                if (banner.FeatureId == feature_id)
                {
                    foreach (BannerTag btag in cache.BannerCache.Keys)
                    {
                        if (btag.BannerId == banner.Id)
                        {
                            return new BannerDto { BannerId = banner.Id, Content = banner.Content, IsActive = banner.IsActive };
                        }
                    }
                }
            }


            var dbBanner = _bannerService.GetUserBanner(tag_id, feature_id);
            if (!dbBanner.HasValue)
            {
                return null;
            }

            cache.BannerCache.Add(dbBanner.Value.Key, dbBanner.Value.Value);
            return new BannerDto { BannerId = dbBanner.Value.Value.Id, Content = dbBanner.Value.Value.Content, IsActive = dbBanner.Value.Value.IsActive };
        }


        public async Task UpdateBanner(BannerDto banner, int id)
        {
            UCache.BannerCache.Add(id, banner);
        }
    }
}
