using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DatPhong
{
    [Key]
    public int MaDatPhong { get; set; }

    public int MaKhachHang { get; set; }

    public DateTime NgayDat { get; set; }

    public decimal TongTien { get; set; }

    public string TrangThai { get; set; } = string.Empty;

    public string? GhiChu { get; set; }

    [ForeignKey(nameof(MaKhachHang))]
    public KhachHang? KhachHang { get; set; }

    public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();

    public ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}