using HotelBookingSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin
        public IActionResult Index()
        {
            // Thống kê phòng
            int tongPhong = _context.Phongs.Count();
            int phongTrong = _context.Phongs.Count(p => p.TrangThai == "Trống");
            int phongDaDat = _context.Phongs.Count(p => p.TrangThai == "Đã đặt");

            // Thống kê khách hàng
            int tongKhachHang = _context.KhachHangs.Count();

            // Thống kê đặt phòng
            int tongDonDatPhong = _context.DatPhongs.Count();

            int donChoXacNhan = _context.DatPhongs
                .Count(d => d.TrangThai == "Chờ xác nhận");

            int donDaXacNhan = _context.DatPhongs
                .Count(d => d.TrangThai == "Đã xác nhận");

            int donDaHuy = _context.DatPhongs
                .Count(d => d.TrangThai == "Đã hủy");

            ViewBag.TongPhong = tongPhong;
            ViewBag.PhongTrong = phongTrong;
            ViewBag.PhongDaDat = phongDaDat;
            ViewBag.TongKhachHang = tongKhachHang;
            ViewBag.TongDonDatPhong = tongDonDatPhong;
            ViewBag.DonChoXacNhan = donChoXacNhan;
            ViewBag.DonDaXacNhan = donDaXacNhan;
            ViewBag.DonDaHuy = donDaHuy;

            return View();
        }
    }
}