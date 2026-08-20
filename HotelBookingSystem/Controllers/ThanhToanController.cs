using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelBookingSystem.Controllers
{
    [Authorize]
    public class ThanhToanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThanhToanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ThanhToan/Create?id=1
        [HttpGet]
        public IActionResult Create(int id)
        {
            var maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var datPhong = _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefault(d =>
                    d.MaDatPhong == id &&
                    d.KhachHang!.MaTaiKhoan == maTaiKhoan);

            if (datPhong == null)
            {
                return NotFound();
            }

            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["Error"] =
                    "Đơn đặt phòng chưa được xác nhận hoặc không thể thanh toán.";

                return RedirectToAction("BookingHistory", "Home");
            }

            var daThanhToan = _context.ThanhToans
                .Any(t => t.MaDatPhong == id &&
                          t.TrangThai == "Đã thanh toán");

            if (daThanhToan)
            {
                TempData["Error"] =
                    "Đơn đặt phòng này đã được thanh toán.";

                return RedirectToAction("BookingHistory", "Home");
            }

            var thanhToan = _context.ThanhToans
                .FirstOrDefault(t => t.MaDatPhong == id);

            if (thanhToan == null)
            {
                thanhToan = new ThanhToan
                {
                    MaDatPhong = datPhong.MaDatPhong,
                    SoTien = datPhong.TongTien,
                    PhuongThucThanhToan = "Tiền mặt",
                    TrangThai = "Chưa thanh toán"
                };
            }

            ViewBag.DatPhong = datPhong;

            return View(thanhToan);
        }

        // POST: /ThanhToan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
    ThanhToan model,
    string phuongThucThanhToan)
        {
            var maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var datPhong = _context.DatPhongs
                .Include(d => d.KhachHang)
                .FirstOrDefault(d =>
                    d.MaDatPhong == model.MaDatPhong &&
                    d.KhachHang!.MaTaiKhoan == maTaiKhoan);

            if (datPhong == null)
            {
                return NotFound();
            }

            // Chỉ cho thanh toán đơn đã xác nhận
            if (datPhong.TrangThai != "Đã xác nhận")
            {
                TempData["Error"] =
                    "Đơn đặt phòng chưa được xác nhận.";

                return RedirectToAction("BookingHistory", "Home");
            }

            // Không cho thanh toán lần 2
            var daThanhToan = _context.ThanhToans
                .Any(t => t.MaDatPhong == model.MaDatPhong &&
                          t.TrangThai == "Đã thanh toán");

            if (daThanhToan)
            {
                TempData["Error"] =
                    "Đơn đặt phòng này đã được thanh toán.";

                return RedirectToAction("BookingHistory", "Home");
            }

            // Bắt buộc chọn phương thức thanh toán
            if (string.IsNullOrEmpty(phuongThucThanhToan))
            {
                ModelState.AddModelError(
                    "PhuongThucThanhToan",
                    "Vui lòng chọn phương thức thanh toán."
                );

                ViewBag.DatPhong = datPhong;
                return View(model);
            }

            var thanhToan = _context.ThanhToans
                .FirstOrDefault(t =>
                    t.MaDatPhong == model.MaDatPhong);

            if (thanhToan == null)
            {
                thanhToan = new ThanhToan
                {
                    MaDatPhong = datPhong.MaDatPhong,
                    SoTien = datPhong.TongTien
                };

                _context.ThanhToans.Add(thanhToan);
            }

            thanhToan.SoTien = datPhong.TongTien;
            thanhToan.PhuongThucThanhToan = phuongThucThanhToan;
            thanhToan.NgayThanhToan = DateTime.Now;
            thanhToan.TrangThai = "Đã thanh toán";

            _context.SaveChanges();

            TempData["Success"] = "Thanh toán thành công.";

            return RedirectToAction(
                "BookingHistory",
                "Home");
        }
    }
}