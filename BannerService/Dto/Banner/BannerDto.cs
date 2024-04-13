namespace BannerService.Dto.Banner
{
    public class BannerDto
    {
        public int BannerId { get; set; }
        public int[]? TagIds { get; set; }
        public int? FeatureId { get; set; }
        public string Content { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
