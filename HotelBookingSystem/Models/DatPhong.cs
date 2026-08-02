using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Models
{
    public class DatPhong
    {
        [Key]
        public int MaDatPhong { get; set; }

        [Required]
        public int MaKhachHang { get; set; }

        [Required]
        [Display(Name = "Ngày đặt")]
        [DataType(DataType.DateTime)]
        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0.")]
        [Display(Name = "Tổng tiền")]
        public decimal TongTien { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống.")]
        [StringLength(30)]
        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; } = "Chờ xác nhận";

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }

        // Navigation Property
        [ForeignKey(nameof(MaKhachHang))]
        public KhachHang? KhachHang { get; set; }

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();

        public ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
    }
}