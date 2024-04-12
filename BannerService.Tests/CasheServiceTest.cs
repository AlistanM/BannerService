using NUnit.Framework;
using BannerService.Services;
using Assert = Xunit.Assert;
using BannerService.Dto.Banner;

namespace BannerService.Tests
{
    [TestFixture]
    public class CasheServiceTest
    {
        private readonly CasheService _casheService;

        public CasheServiceTest(CasheService casheService)
        {
            _casheService = casheService;
        }

        [Test]
        public async Task GetUserBannerTestSuccess()
        {
            var tagId = 3;
            var featureId = 5;

            var banner = _casheService.GetUserBanner(tagId, featureId);
            Assert.NotNull(banner);
        }

        [Test]
        public async Task GetUserBannerTestFail()
        {
            var tagId = 0;
            var featureId = -1;

            var banner = _casheService.GetUserBanner(tagId, featureId);
            Assert.Null(banner);
        }
    }
}