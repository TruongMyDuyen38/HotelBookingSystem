using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class ChiTietDatPhong
    {
        [Key]
        public int MaChiTiet { get; set; }

        [Required]
        public int MaDatPhong { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày nhận phòng")]
        public DateTime NgayNhanPhong { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả phòng")]
        public DateTime NgayTraPhong { get; set; }

        [Range(1, 20, ErrorMessage = "Số người phải từ 1 đến 20.")]
        [Display(Name = "Số người")]
        public int SoNguoi { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0.")]
        [Display(Name = "Đơn giá")]
        public decimal DonGia { get; set; }

        // Navigation Property
        [ForeignKey(nameof(MaDatPhong))]
        public DatPhong? DatPhong { get; set; }

        [ForeignKey(nameof(MaPhong))]
        public Phong? Phong { get; set; }
    }
}