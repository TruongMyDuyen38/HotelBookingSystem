using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Rooms
        [HttpGet]
        public IActionResult Rooms()
        {
            var dsPhong = _context.Phongs
                .Include(p => p.LoaiPhong)
                .ToList();

            return View(dsPhong);
        }
        // GET: /Home/RoomDetails
        [HttpGet]
        public IActionResult RoomDetails(int id)
        {
            var phong = _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefault(p => p.MaPhong == id);

            if (phong == null)
            {
                return NotFound();
            }

            var dsAnh = _context.HinhAnhPhongs
                .Where(h => h.MaPhong == id)
                .ToList();

            var viewModel = new PhongHinhAnhViewModel
            {
                Phong = phong,
                DanhSachAnh = dsAnh
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}