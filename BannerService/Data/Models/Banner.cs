using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerService.Data.Models
{
    [Table("Banners")]
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public int TagId { get; set; }
        public int FeatureId { get; set; }
        public string Content { get; set; }
    }
}
