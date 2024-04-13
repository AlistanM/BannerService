using BannerService.Data;
using BannerService.Services;
using Quartz;

namespace BannerService.Jobs
{
    public class UpdateJob : IJob
    {
        private readonly BannerDtoService _bannerService;

        public UpdateJob(BannerDtoService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var cache = CacheService.UCache.BannerCache;
            await _bannerService.UpdateBanner(cache);
        }
    }
}