using NUnit.Framework;
using BannerService.Services;
using Assert = Xunit.Assert;
using BannerService.Dto.Banner;

namespace BannerService.Tests
{
    [TestFixture]
    public class CacheServiceTest
    {
        private readonly CacheService _cacheService;

        public CacheServiceTest(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [Test]
        public async Task GetUserBannerTestSuccess()
        {
            var tagId = 3;
            var featureId = 5;

            var banner = _cacheService.GetUserBanner(tagId, featureId);
            Assert.NotNull(banner);
        }

        [Test]
        public async Task GetUserBannerTestFail()
        {
            var tagId = 0;
            var featureId = -1;

            var banner = _cacheService.GetUserBanner(tagId, featureId);
            Assert.Null(banner);
        }
    }
}