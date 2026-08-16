using HotelBookingSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /KhachHang
        [HttpGet]
        public IActionResult Index()
        {
            var danhSachKhachHang = _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .OrderBy(k => k.MaKhachHang)
                .ToList();

            return View(danhSachKhachHang);
        }

        // GET: /KhachHang/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var khachHang = _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .Include(k => k.DatPhongs)
                .FirstOrDefault(k => k.MaKhachHang == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
    }
}