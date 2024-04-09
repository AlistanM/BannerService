using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BannerService.Dto.Banner
{
    public class BannerDto
    {
        [JsonProperty("banner_id")]
        public int BannerId { get; set; }

        [JsonProperty("tag_ids")]
        public int[]? TagIds { get; set; }

        [JsonProperty("feature_id")]
        public int? FeatureId { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
