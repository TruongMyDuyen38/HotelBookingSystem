using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaiKhoan
{
    [Key]
    public int MaTaiKhoan { get; set; }

    [Required]
    [StringLength(50)]
    public string TenDangNhap { get; set; } = string.Empty;

    [Required]
    public string MatKhau { get; set; } = string.Empty;

    public int MaVaiTro { get; set; }

    [ForeignKey(nameof(MaVaiTro))]
    public VaiTro? VaiTro { get; set; }

    public bool TrangThai { get; set; }

    public KhachHang? KhachHang { get; set; }
}