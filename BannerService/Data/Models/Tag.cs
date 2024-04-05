using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerService.Data.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public  int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
