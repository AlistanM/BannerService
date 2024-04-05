using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BannerService.ApiModels
{
    public class BannerDto
    {
        [JsonProperty("tag_ids")]
        public int[]? TagIds { get; set; }

        [JsonProperty("feature_id")]
        public int? FeatureId { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }
    }
}
