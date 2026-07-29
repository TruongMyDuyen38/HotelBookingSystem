using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HinhAnhPhong
{
    [Key]
    public int MaHinhAnh { get; set; }

    public int MaPhong { get; set; }

    public string DuongDanAnh { get; set; } = string.Empty;

    [ForeignKey(nameof(MaPhong))]
    public Phong? Phong { get; set; }
}