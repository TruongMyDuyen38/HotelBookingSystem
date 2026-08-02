using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Số phòng không được để trống.")]
        [StringLength(10)]
        public string SoPhong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên phòng không được để trống.")]
        [StringLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [Required]
        public int MaLoaiPhong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal GiaMotDem { get; set; }

        [Range(1, 20)]
        public int SucChua { get; set; }

        [StringLength(500)]
        public string? MoTa { get; set; }

        [Required]
        [StringLength(30)]
        public string TrangThai { get; set; } = string.Empty;

        // Navigation Property
        [ForeignKey(nameof(MaLoaiPhong))]
        public LoaiPhong? LoaiPhong { get; set; }

        public ICollection<HinhAnhPhong> HinhAnhPhongs { get; set; } = new List<HinhAnhPhong>();

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();
    }
}