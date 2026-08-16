using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DatPhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatPhongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /DatPhong
        [HttpGet]
        public IActionResult Index()
        {
            var danhSachDatPhong = _context.DatPhongs
                .Include(d => d.KhachHang)
                .Include(d => d.ChiTietDatPhongs)
                    .ThenInclude(c => c.Phong)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(danhSachDatPhong);
        }
        // POST: /DatPhong/Confirm
        [HttpPost]
        public IActionResult Confirm(int id)
        {
            var datPhong = _context.DatPhongs
                .FirstOrDefault(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return NotFound();
            }

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["Error"] = "Đơn này không thể xác nhận.";
                return RedirectToAction("Index");
            }

            datPhong.TrangThai = "Đã xác nhận";

            _context.SaveChanges();

            TempData["Success"] = "Xác nhận đặt phòng thành công.";

            return RedirectToAction("Index");
        }


        // POST: /DatPhong/Cancel
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var datPhong = _context.DatPhongs
                .Include(d => d.ChiTietDatPhongs)
                .FirstOrDefault(d => d.MaDatPhong == id);

            if (datPhong == null)
            {
                return NotFound();
            }

            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["Error"] = "Đơn này không thể hủy.";
                return RedirectToAction("Index");
            }

            datPhong.TrangThai = "Đã hủy";

            // Trả phòng về trạng thái Trống
            foreach (var chiTiet in datPhong.ChiTietDatPhongs)
            {
                var phong = _context.Phongs
                    .FirstOrDefault(p => p.MaPhong == chiTiet.MaPhong);

                if (phong != null)
                {
                    phong.TrangThai = "Trống";
                }
            }

            _context.SaveChanges();

            TempData["Success"] = "Hủy đặt phòng thành công.";

            return RedirectToAction("Index");
        }
    }
}