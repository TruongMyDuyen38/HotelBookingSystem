using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }

        [Required(ErrorMessage = "Tên loại phòng không được để trống.")]
        [StringLength(100)]
        [Display(Name = "Tên loại phòng")]
        public string TenLoaiPhong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá cơ bản không được để trống.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        [Display(Name = "Giá cơ bản")]
        public decimal GiaCoBan { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        // Navigation Property
        public ICollection<Phong> Phongs { get; set; } = new List<Phong>();
    }
}