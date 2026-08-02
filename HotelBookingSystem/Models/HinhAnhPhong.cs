using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class HinhAnhPhong
    {
        [Key]
        public int MaHinhAnh { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống.")]
        [StringLength(255)]
        [Display(Name = "Đường dẫn ảnh")]
        public string DuongDanAnh { get; set; } = string.Empty;

        // Navigation Property
        [ForeignKey(nameof(MaPhong))]
        public Phong? Phong { get; set; }
    }
}