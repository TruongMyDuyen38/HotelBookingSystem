using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChiTietDatPhong
{
    [Key]
    public int MaChiTiet { get; set; }

    public int MaDatPhong { get; set; }

    public int MaPhong { get; set; }

    public DateTime NgayNhanPhong { get; set; }

    public DateTime NgayTraPhong { get; set; }

    public int SoNguoi { get; set; }

    public decimal DonGia { get; set; }

    [ForeignKey(nameof(MaDatPhong))]
    public DatPhong? DatPhong { get; set; }

    [ForeignKey(nameof(MaPhong))]
    public Phong? Phong { get; set; }
}