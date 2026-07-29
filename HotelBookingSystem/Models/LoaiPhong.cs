using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;

public class LoaiPhong
{
    [Key]
    public int MaLoaiPhong { get; set; }

    [Required]
    [StringLength(100)]
    public string TenLoaiPhong { get; set; } = string.Empty;

    public decimal GiaCoBan { get; set; }

    public string? MoTa { get; set; }

    public ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}