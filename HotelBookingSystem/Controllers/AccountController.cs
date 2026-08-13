using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotelBookingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra tên đăng nhập đã tồn tại chư
            bool daTonTai = _context.TaiKhoans
                .Any(t => t.TenDangNhap == model.TenDangNhap);

            if (daTonTai)
            {
                ModelState.AddModelError("TenDangNhap",
                    "Tên đăng nhập đã tồn tại");

                return View(model);
            }

            // Tạo tài khoản
            var taiKhoan = new TaiKhoan
            {
                TenDangNhap = model.TenDangNhap,
                MatKhau = model.MatKhau, 
                MaVaiTro = 2, // 2 = Khách hàng
                TrangThai = true
            };

            _context.TaiKhoans.Add(taiKhoan);
            _context.SaveChanges();

            // Tạo khách hàng
            var khachHang = new KhachHang
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = model.HoTen,
                Email = model.Email,
                SoDienThoai = model.SoDienThoai
            };

            _context.KhachHangs.Add(khachHang);
            _context.SaveChanges();

            TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";

            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra tài khoản
            var taiKhoan = _context.TaiKhoans.FirstOrDefault(t =>
                t.TenDangNhap == model.TenDangNhap &&
                t.MatKhau == model.MatKhau);

            if (taiKhoan == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                return View(model);
            }

            if (!taiKhoan.TrangThai)
            {
                ModelState.AddModelError("", "Tài khoản đã bị khóa.");
                return View(model);
            }

            // Lấy tên vai trò
            var vaiTro = _context.VaiTros.FirstOrDefault(v => v.MaVaiTro == taiKhoan.MaVaiTro);

            string tenVaiTro = vaiTro?.TenVaiTro ?? "Khách hàng";

            // Tạo Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, taiKhoan.MaTaiKhoan.ToString()),
        new Claim(ClaimTypes.Name, taiKhoan.TenDangNhap),
        new Claim(ClaimTypes.Role, tenVaiTro)
    };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            // Ghi Cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            TempData["Success"] = "Đăng nhập thành công!";

            return RedirectToAction("Index", "Home");
        }
        // Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        // GET: /Account/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login");
            }

            int maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var khachHang = _context.KhachHangs
                .Include(k => k.TaiKhoan)
                .FirstOrDefault(k => k.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                MaKhachHang = khachHang.MaKhachHang,
                HoTen = khachHang.HoTen,
                Email = khachHang.Email,
                SoDienThoai = khachHang.SoDienThoai,
                TenDangNhap = khachHang.TaiKhoan?.TenDangNhap ?? ""
            };

            return View(model);
        }
        // POST: /Account/Profile
        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int maTaiKhoan = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var khachHang = _context.KhachHangs
                .FirstOrDefault(k =>
                    k.MaKhachHang == model.MaKhachHang &&
                    k.MaTaiKhoan == maTaiKhoan);

            if (khachHang == null)
            {
                return NotFound();
            }

            khachHang.HoTen = model.HoTen;
            khachHang.Email = model.Email;
            khachHang.SoDienThoai = model.SoDienThoai;

            _context.SaveChanges();

            TempData["Success"] = "Cập nhật thông tin thành công.";

            return RedirectToAction("Profile");
        }
    }
}