using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerService.Data.Models
{
    [Table("BannerTag")]
    [PrimaryKey(nameof(BannerId), nameof(TagId))]
    public class BannerTag
    {
        public int BannerId { get; set; }
        public int TagId { get; set; }
    }
}
