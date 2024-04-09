using BannerService.Data;
using BannerService.Data.Models;
using BannerService.Dto.Banner;

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
            var banner = _db.Banners
                .Where(fid => fid.FeaturesId == feature_id)
                .Join(_db.BannerTag,
                bid => bid.Id,
                tid => tid.BannerId,
                (bid, tid) => new { Bid = bid, Tid = tid })
                .Where(x => x.Tid.TagId == tag_id && x.Tid.BannerId == x.Bid.Id).FirstOrDefault();


            if (banner == null)
            {
                return null;
            }
            return new BannerDto { BannerId = banner.Bid.Id, Content = banner.Bid.Content, IsActive = banner.Bid.IsActive };
        }

        public BannerDto[]? GetAdminBanners(int tag_id, int feature_id)
        {
            var banners = _db.Banners
                .Where(fid => fid.FeaturesId == feature_id)
                .Join(_db.BannerTag,
                bid => bid.Id,
                tid => tid.BannerId,
                (bid, tid) => new { Bid = bid, Tid = tid })
                .Where(x => x.Tid.TagId == tag_id && x.Tid.BannerId == x.Bid.Id)
                .Select(newBanner => new BannerDto
                {
                    BannerId = newBanner.Bid.Id,
                    Content = newBanner.Bid.Content,
                    IsActive = newBanner.Bid.IsActive,
                    FeatureId = newBanner.Bid.FeaturesId,
                    CreatedAt = newBanner.Bid.CreatedAt,
                    UpdatedAt = newBanner.Bid.UpdatedAt,
                    TagIds = _db.BannerTag.Where(x => x.BannerId == newBanner.Bid.Id).Select(y => y.TagId).ToArray()
                }
                ).ToArray();


            if (banners == null)
            {
                return null;
            }

            return banners;
        }

        public async Task ToBanner(BannerDto banner)
        {
            if (!_db.Features.Any(x => x.Id == banner.FeatureId))
            {
                throw new Exception("Фича с таким id не найдена");
            }

            var newBanner = new Banner()
            {
                FeaturesId = (int)banner.FeatureId,
                Content = banner.Content,
                CreatedAt = DateTime.Now,
                IsActive = (bool)banner.IsActive
            };

            _db.Banners.Add(newBanner);
            await _db.SaveChangesAsync();

            int bannerId = newBanner.Id;
            await CreateTag(bannerId, banner.TagIds);
        }

        public async void CreateFeature(int feature_id, Banner banner)
        {

        }

        public async Task CreateTag(int bannerId, int[] tagIds)
        {
            foreach (int id in tagIds)
            {
                if (!_db.Tags.Any(x => x.Id == id))
                {
                    throw new Exception("Тэг с таким id не найден");
                }

                var bt = new BannerTag() { BannerId = bannerId, TagId = id };
                _db.BannerTag.Add(bt);
            }

            await _db.SaveChangesAsync();
        }

        public async Task UpdateBanner(BannerDto banner, int id)
        {
            var uBanner = new Banner() { Id = id, Content = banner.Content, UpdatedAt = DateTime.Now, FeaturesId = (int)banner.FeatureId, IsActive = (bool)banner.IsActive };
            _db.Attach(uBanner);

            var bannerTag = _db.BannerTag.Where(x=> x.BannerId == id).ToArray();
            _db.RemoveRange(bannerTag);
            await CreateTag(id, banner.TagIds);

            _db.Banners.Update(uBanner);
            _db.Entry(uBanner).Property(x => x.CreatedAt).IsModified = false;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteBanner(int id)
        {
            var banner = _db.Banners.Where(x => x.Id == id).FirstOrDefault();
            _db.Banners.Remove(banner);

            var bannerTag = _db.BannerTag.Where(x => x.BannerId == id).ToArray();
            _db.RemoveRange(bannerTag);

            await _db.SaveChangesAsync();
        }
    }
}
