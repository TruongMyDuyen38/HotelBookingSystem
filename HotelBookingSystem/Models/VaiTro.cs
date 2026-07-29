using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;

public class VaiTro
{
    [Key]
    public int MaVaiTro { get; set; }

    [Required]
    [StringLength(50)]
    public string TenVaiTro { get; set; } = string.Empty;

    public ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}