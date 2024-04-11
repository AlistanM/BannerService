using BannerService.Cashe;
using BannerService.Data.Models;
using BannerService.Dto.Banner;

namespace BannerService.Services
{
    public class CasheService
    {
        private readonly BannerDtoService _bannerService;
        private static GetCashe cashe = new GetCashe();
        public static UpdateCashe uCashe = new UpdateCashe();

        public CasheService(BannerDtoService bannerService)
        {
            _bannerService = bannerService;
        }

        public BannerDto? GetUserBanner(int tag_id, int feature_id)
        {

            foreach (Banner banner in cashe.bannerCashe.Values)
            {
                if (banner.FeaturesId == feature_id)
                {
                    foreach (BannerTag btag in cashe.bannerCashe.Keys)
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

            cashe.bannerCashe.Add(dbBanner.Value.Key, dbBanner.Value.Value);
            return new BannerDto { BannerId = dbBanner.Value.Value.Id, Content = dbBanner.Value.Value.Content, IsActive = dbBanner.Value.Value.IsActive };
        }


        public async Task UpdateBanner(BannerDto banner, int id)
        {
            uCashe.bannerCashe.Add(id, banner);
        }
    }
}
