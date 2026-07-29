using HotelBookingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Phong
{
    [Key]
    public int MaPhong { get; set; }

    public string SoPhong { get; set; } = string.Empty;

    public string TenPhong { get; set; } = string.Empty;

    public int MaLoaiPhong { get; set; }

    public decimal GiaMotDem { get; set; }

    public int SucChua { get; set; }

    public string? MoTa { get; set; }

    public string TrangThai { get; set; } = string.Empty;

    [ForeignKey(nameof(MaLoaiPhong))]
    public LoaiPhong? LoaiPhong { get; set; }

    public ICollection<HinhAnhPhong> HinhAnhPhongs { get; set; } = new List<HinhAnhPhong>();

    public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();
}