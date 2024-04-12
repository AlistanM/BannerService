using BannerService.Data;
using BannerService.Data.Models;
using BannerService.Dto.Banner;
using System.Reflection;

namespace BannerService.Services
{
    public class BannerDtoService
    {
        private readonly DataContext _db;
        public BannerDtoService(DataContext db)
        {
            _db = db;
        }

        public KeyValuePair<BannerTag, Banner>? GetUserBanner(int tagId, int featureId)
        {
            var banner = _db.Banners
                .Where(fid => fid.FeaturesId == featureId)
                .Join(_db.BannerTag,
                bid => bid.Id,
                tid => tid.BannerId,
                (bid, tid) => new { Bid = bid, Tid = tid })
                .Where(x => x.Tid.TagId == tagId && x.Tid.BannerId == x.Bid.Id).FirstOrDefault();

            if (banner == null)
            {
                return null;
            }

            var tag = _db.BannerTag.Where(x => x.BannerId == banner.Bid.Id && x.TagId == banner.Tid.TagId).FirstOrDefault();

            return new KeyValuePair<BannerTag, Banner>(tag, banner.Bid);
        }

        public BannerDto[]? GetAdminBanners(int tagId, int featureId, int limit)
        {
            var banners = _db.Banners
                .Where(fid => fid.FeaturesId == featureId)
                .Join(_db.BannerTag,
                bid => bid.Id,
                tid => tid.BannerId,
                (bid, tid) => new { Bid = bid, Tid = tid })
                .Where(x => x.Tid.TagId == tagId && x.Tid.BannerId == x.Bid.Id)
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
                ).Take(limit).ToArray();


            if (banners == null)
            {
                return null;
            }

            return banners;
        }

        public async Task CreateBanner(BannerDto banner)
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

        public async Task UpdateBanner(Dictionary<int,BannerDto> cashe)
        {
            var banners = new List<Banner>();

            foreach(var b in cashe)
            {
                var ub = new Banner()
                {
                    Id = b.Key,
                    Content = b.Value.Content,
                    UpdatedAt = DateTime.Now,
                    FeaturesId = (int)b.Value.FeatureId,
                    IsActive = (bool)b.Value.IsActive
                };
                banners.Add(ub);

                var bannerTag = _db.BannerTag.Where(x => x.BannerId == b.Key).ToArray();
                _db.RemoveRange(bannerTag);
                await CreateTag(b.Key, b.Value.TagIds);
            }

            _db.AttachRange(banners);
            _db.Banners.UpdateRange(banners);
            foreach(var b in banners)
                _db.Entry(b).Property(x => x.CreatedAt).IsModified = false;

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
