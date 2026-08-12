using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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
        // GET: /Home/Booking
        [HttpGet]
        public IActionResult Booking(int id)
        {
            // Kiểm tra khách đã đăng nhập chưa
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy phòng
            var phong = _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefault(p => p.MaPhong == id);

            if (phong == null)
            {
                return NotFound();
            }

            // Tạo ViewModel
            var viewModel = new DatPhongViewModel
            {
                Phong = phong,
                NgayNhanPhong = DateTime.Today,
                NgayTraPhong = DateTime.Today.AddDays(1),
                SoNguoi = 1
            };

            return View(viewModel);
        }
        // POST: /Home/Booking
        [HttpPost]
        public IActionResult Booking(int MaPhong, DatPhongViewModel model)
        {
            // Kiểm tra người dùng đã đăng nhập
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra phòng
            var phong = _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefault(p => p.MaPhong == MaPhong);

            if (phong == null)
            {
                return NotFound();
            }
            ModelState.Remove(nameof(DatPhongViewModel.Phong));

            if (phong.TrangThai != "Trống")
            {
                TempData["Error"] = "Phòng này hiện không còn trống.";
                return RedirectToAction("Rooms");
            }

            // Kiểm tra ngày
            if (model.NgayNhanPhong < DateTime.Today)
            {
                ModelState.AddModelError(
                    "NgayNhanPhong",
                    "Ngày nhận phòng không được trước ngày hiện tại."
                );
            }

            if (model.NgayTraPhong <= model.NgayNhanPhong)
            {
                ModelState.AddModelError(
                    "NgayTraPhong",
                    "Ngày trả phòng phải sau ngày nhận phòng."
                );
            }

            // Kiểm tra số người
            if (model.SoNguoi < 1 || model.SoNguoi > phong.SucChua)
            {
                ModelState.AddModelError(
                    "SoNguoi",
                    $"Số người phải từ 1 đến {phong.SucChua}."
                );
            }

            // Nếu có lỗi thì hiển thị lại form
            if (!ModelState.IsValid)
            {
                model.Phong = phong;
                return View(model);
            }

            // Tính số đêm
            int soDem = (model.NgayTraPhong - model.NgayNhanPhong).Days;

            // Tính tổng tiền
            decimal tongTien = soDem * phong.GiaMotDem;

            // Lấy MaKhachHang từ tài khoản đang đăng nhập
            int maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var khachHang = _context.KhachHangs
                .FirstOrDefault(k => k.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            // Tạo đơn đặt phòng
            var datPhong = new DatPhong
            {
                MaKhachHang = khachHang.MaKhachHang,
                NgayDat = DateTime.Now,
                TongTien = tongTien,
                TrangThai = "Chờ xác nhận",
                GhiChu = model.GhiChu
            };

            _context.DatPhongs.Add(datPhong);
            _context.SaveChanges();

            // Tạo chi tiết đặt phòng
            var chiTiet = new ChiTietDatPhong
            {
                MaDatPhong = datPhong.MaDatPhong,
                MaPhong = phong.MaPhong,
                NgayNhanPhong = model.NgayNhanPhong,
                NgayTraPhong = model.NgayTraPhong,
                SoNguoi = model.SoNguoi,
                DonGia = phong.GiaMotDem
            };

            _context.ChiTietDatPhongs.Add(chiTiet);

            // Cập nhật trạng thái phòng
            phong.TrangThai = "Đã đặt";

            _context.SaveChanges();

            TempData["Success"] = "Đặt phòng thành công! Vui lòng chờ xác nhận.";

            return RedirectToAction("BookingHistory");
        }
        // GET: /Home/BookingHistory
        [HttpGet]
        public IActionResult BookingHistory()
        {
            // Kiểm tra đăng nhập
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy mã tài khoản đang đăng nhập
            int maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            // Tìm khách hàng tương ứng
            var khachHang = _context.KhachHangs
                .FirstOrDefault(k => k.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            // Lấy lịch sử đặt phòng của khách hàng
            var danhSachDatPhong = _context.DatPhongs
                .Include(d => d.ChiTietDatPhongs)
                    .ThenInclude(c => c.Phong)
                .Where(d => d.MaKhachHang == khachHang.MaKhachHang)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(danhSachDatPhong);
        }
        // POST: /Home/CancelBooking
        [HttpPost]
        public IActionResult CancelBooking(int id)
        {
            // Kiểm tra đăng nhập
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy mã tài khoản đang đăng nhập
            int maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            // Tìm khách hàng
            var khachHang = _context.KhachHangs
                .FirstOrDefault(k => k.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            // Tìm đơn đặt phòng của chính khách hàng
            var datPhong = _context.DatPhongs
                .Include(d => d.ChiTietDatPhongs)
                .FirstOrDefault(d =>
                    d.MaDatPhong == id &&
                    d.MaKhachHang == khachHang.MaKhachHang);

            if (datPhong == null)
            {
                return NotFound("Không tìm thấy đơn đặt phòng.");
            }

            // Chỉ cho hủy khi đang chờ xác nhận
            if (datPhong.TrangThai != "Chờ xác nhận")
            {
                TempData["Error"] =
                    "Đơn đặt phòng này không thể hủy.";

                return RedirectToAction("BookingHistory");
            }

            // Đổi trạng thái đơn
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

            TempData["Success"] =
                "Hủy đặt phòng thành công.";

            return RedirectToAction("BookingHistory");
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