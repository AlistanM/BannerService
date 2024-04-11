using BannerService.Data;
using BannerService.Services;
using Quartz;

namespace BannerService.Jobs
{
    public class UpdateJob : IJob
    {
        private readonly DataContext _db;
        private readonly BannerDtoService _bannerService;

        public UpdateJob(DataContext db, BannerDtoService bannerService)
        {
            _db = db;
            _bannerService = bannerService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var cashe = CasheService.uCashe.bannerCashe;
            await _bannerService.UpdateBanner(cashe);
        }
    }
}