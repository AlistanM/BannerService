using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerService.Data.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Login { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}
