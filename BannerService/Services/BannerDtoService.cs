using BannerService.Data;
using BannerService.Data.Models;
using BannerService.Dto.Banner;

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
                .Where(fid => fid.FeatureId == featureId)
                .Join(_db.BannerTag,
                bid => bid.Id,
                tid => tid.BannerId,
                (bid, tid) => new { Bid = bid, Tid = tid })
                .Where(x => x.Tid.TagId == tagId && x.Tid.BannerId == x.Bid.Id).FirstOrDefault();

            if (banner == null || banner.Bid.IsDeleted == true)
            {
                return null;
            }

            var tag = _db.BannerTag.Where(x => x.BannerId == banner.Bid.Id && x.TagId == banner.Tid.TagId).FirstOrDefault();

            return new KeyValuePair<BannerTag, Banner>(tag, banner.Bid);
        }

        public BannerDto[]? GetAdminBanners(int tagId, int featureId, int limit)
        {
            var banners = _db.Banners
                .Where(fid => fid.FeatureId == featureId)
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
                    FeatureId = newBanner.Bid.FeatureId,
                    CreatedAt = newBanner.Bid.CreatedAt,
                    UpdatedAt = newBanner.Bid.UpdatedAt,
                    IsDeleted = newBanner.Bid.IsDeleted,
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
                throw new Exception("The feature with this id was not found");
            }

            var newBanner = new Banner()
            {
                FeatureId = (int)banner.FeatureId,
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
                    throw new Exception("The tag with this id was not found");
                }

                var bt = new BannerTag() { BannerId = bannerId, TagId = id };
                _db.BannerTag.Add(bt);
            }

            await _db.SaveChangesAsync();
        }

        public async Task UpdateBanner(Dictionary<int, BannerDto> cache)
        {
            var banners = new List<Banner>();

            foreach (var b in cache)
            {
                var ub = new Banner()
                {
                    Id = b.Key,
                    Content = b.Value.Content,
                    UpdatedAt = DateTime.Now,
                    FeatureId = (int)b.Value.FeatureId,
                    IsActive = (bool)b.Value.IsActive
                };
                banners.Add(ub);

                var bannerTag = _db.BannerTag.Where(x => x.BannerId == b.Key).ToArray();
                _db.RemoveRange(bannerTag);
                await CreateTag(b.Key, b.Value.TagIds);
            }

            _db.AttachRange(banners);
            _db.Banners.UpdateRange(banners);
            foreach (var b in banners)
                _db.Entry(b).Property(x => x.CreatedAt).IsModified = false;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteBanner(int id)
        {
            var banner = _db.Banners.Where(x => x.Id == id).FirstOrDefault();
            banner.IsDeleted = true;

            _db.Banners.Update(banner);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteBannerByTagAndFeature(int featureId = 0, int tagId = 0)
        {
            if (tagId == 0)
            {
                var banners = _db.Banners.Where(x => x.FeatureId == featureId).ToArray();

                foreach (var b in banners)
                {
                    await DeleteBanner(b.Id);
                }
            }

            if (featureId == 0)
            {
                var bannerTag = _db.BannerTag.Where(x => x.TagId == tagId).ToArray();
                foreach (var b in bannerTag)
                {
                    await DeleteBanner(b.BannerId);
                }
            }
            if (tagId != 0 && featureId != 0)
            {
                var banner = GetUserBanner(tagId, featureId);
                await DeleteBanner(banner.Value.Value.Id);
            }
        }
    }
}
