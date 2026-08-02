using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class VaiTro
    {
        [Key]
        public int MaVaiTro { get; set; }

        [Required(ErrorMessage = "Tên vai trò không được để trống.")]
        [StringLength(50)]
        [Display(Name = "Tên vai trò")]
        public string TenVaiTro { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
    }
}