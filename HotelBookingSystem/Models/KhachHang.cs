using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKhachHang { get; set; }

        [Required]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100)]
        [Display(Name = "Họ và tên")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        // Navigation Property
        [ForeignKey(nameof(MaTaiKhoan))]
        public TaiKhoan? TaiKhoan { get; set; }

        public ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
    }
}