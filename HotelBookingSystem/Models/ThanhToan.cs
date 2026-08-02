using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }

        [Required]
        public int MaDatPhong { get; set; }

        [Required(ErrorMessage = "Số tiền không được để trống.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 0.")]
        [Display(Name = "Số tiền")]
        public decimal SoTien { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày thanh toán")]
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống.")]
        [StringLength(50)]
        [Display(Name = "Phương thức thanh toán")]
        public string PhuongThucThanhToan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái không được để trống.")]
        [StringLength(30)]
        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; } = "Chưa thanh toán";

        // Navigation Property
        [ForeignKey(nameof(MaDatPhong))]
        public DatPhong? DatPhong { get; set; }
    }
}