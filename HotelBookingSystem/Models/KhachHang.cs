using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class KhachHang
{
    [Key]
    public int MaKhachHang { get; set; }

    public int MaTaiKhoan { get; set; }

    [Required]
    [StringLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    [ForeignKey(nameof(MaTaiKhoan))]
    public TaiKhoan? TaiKhoan { get; set; }

    public ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
}