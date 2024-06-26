﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BannerService.Data.Models
{
    [Table("Banners")]
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? UpdatedAt { get; set; }
        public int FeatureId { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(FeatureId))]
        public Feature Feature { get; set; }
    }
}
