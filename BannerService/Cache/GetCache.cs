using BannerService.Data.Models;

namespace BannerService.Cache
{
    public class GetCache
    {
        public Dictionary<BannerTag, Banner> BannerCache = new Dictionary<BannerTag, Banner>();
    }
}
