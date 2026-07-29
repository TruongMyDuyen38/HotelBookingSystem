using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ThanhToan
{
    [Key]
    public int MaThanhToan { get; set; }

    public int MaDatPhong { get; set; }

    public decimal SoTien { get; set; }

    public DateTime NgayThanhToan { get; set; }

    public string PhuongThucThanhToan { get; set; } = string.Empty;

    public string TrangThai { get; set; } = string.Empty;

    [ForeignKey(nameof(MaDatPhong))]
    public DatPhong? DatPhong { get; set; }
}