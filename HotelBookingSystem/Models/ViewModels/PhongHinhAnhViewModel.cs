using HotelBookingSystem.Models;

namespace HotelBookingSystem.Models.ViewModels
{
    public class PhongHinhAnhViewModel
    {
        public Phong Phong { get; set; } = null!;

        public List<HinhAnhPhong> DanhSachAnh { get; set; } = new();
    }
}