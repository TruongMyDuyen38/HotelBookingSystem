using HotelBookingSystem.Models;

namespace HotelBookingSystem.Models.ViewModels
{
    public class DatPhongViewModel
    {
        public Phong Phong { get; set; } = null!;

        public DateTime NgayNhanPhong { get; set; }

        public DateTime NgayTraPhong { get; set; }

        public int SoNguoi { get; set; }

        public string? GhiChu { get; set; }
    }
}