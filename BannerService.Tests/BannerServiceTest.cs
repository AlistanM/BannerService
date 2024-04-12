using BannerService.Dto.Banner;
using BannerService.Services;
using NUnit.Framework;
using Assert = Xunit.Assert;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerService.Tests
{
    public class BannerServiceTest
    {

        private readonly BannerDtoService _bannerService;
        public BannerServiceTest(BannerDtoService bannerService)
        {
            _bannerService = bannerService;
        }

        [Test]
        public async Task UpdateBannerTestSuccess()
        {
            var id = 15;
            var banner = new BannerDto { BannerId = id, Content = "Content for test", IsActive = true, FeatureId = 1, TagIds = [1, 2, 3] };

            var d = new Dictionary<int, BannerDto>() { { id, banner } };

            await _bannerService.UpdateBanner(d);

            var b = _bannerService.GetUserBanner(id, 1);
            Assert.Contains("Content for test", b.Value.Value.Content);
        }
        [Test]
        public async Task UpdateBannerTestFail()
        {
            var id = -1;
            var banner = new BannerDto { BannerId = id, Content = "Content for test", IsActive = true, FeatureId = 1, TagIds = [1, 2, 3] };

            var d = new Dictionary<int, BannerDto>() { { id, banner } };

            await _bannerService.UpdateBanner(d);

            var b = _bannerService.GetUserBanner(id, 1);
            Assert.Null(b);
        }

        [Test]
        public async Task CreateBannerTestSuccess()
        {
            var banner = new BannerDto { Content = "Content for test", IsActive = true, FeatureId = -1, TagIds = [1, 2, 3] };

            await _bannerService.CreateBanner(banner);
            var b = _bannerService.GetUserBanner(1, -1);

            Assert.Equal(b.Value.Value.FeaturesId, banner.FeatureId);

            await _bannerService.DeleteBanner(b.Value.Value.Id);
        }



    }
}
