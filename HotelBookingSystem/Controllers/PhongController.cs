using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using HotelBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    public class PhongController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public PhongController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var dsPhong = _context.Phongs.Include(p => p.LoaiPhong).ToList();
            return View(dsPhong);
        }
        //GET: /Phong/Create
        [HttpGet]
        public IActionResult Create()
        {
            var dsLoaiPhong = _context.LoaiPhongs.ToList();

            ViewBag.LoaiPhongList = new SelectList(
                dsLoaiPhong,
                "MaLoaiPhong",
                "TenLoaiPhong"
            );

            return View();
        }
        //POST: /Phong/Create
        [HttpPost]
        public IActionResult Create(Phong model)
        {
            if (!ModelState.IsValid)
            {
                var dsLoaiPhong = _context.LoaiPhongs.ToList();

                ViewBag.LoaiPhongList = new SelectList(
                    dsLoaiPhong,
                    "MaLoaiPhong",
                    "TenLoaiPhong"
                );

                return View(model);
            }

            _context.Phongs.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //GET: /Phong/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var phong = _context.Phongs.Find(id);

            if (phong == null)
            {
                return NotFound();
            }

            ViewBag.LoaiPhongList = new SelectList(
                _context.LoaiPhongs.ToList(),
                "MaLoaiPhong",
                "TenLoaiPhong",
                phong.MaLoaiPhong
            );

            return View(phong);
        }
        //POST: /Phong/Edit
        [HttpPost]
        public IActionResult Edit(Phong model)
        {
            if (!ModelState.IsValid)
            {
                var dsLoaiPhong = _context.LoaiPhongs.ToList();

                ViewBag.LoaiPhongList = new SelectList(
                    dsLoaiPhong,
                    "MaLoaiPhong",
                    "TenLoaiPhong"
                );

                return View(model);
            }

            _context.Phongs.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //GET: /Phong/Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var phong = _context.Phongs
                .Include(p => p.LoaiPhong)
                .FirstOrDefault(p => p.MaPhong == id);

            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }
        //POST: /Phong/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var phong = _context.Phongs.Find(id);
            if (phong == null)
            {
                return NotFound();
            }
            _context.Phongs.Remove(phong);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: /Phong/Details
        [HttpGet]
        public IActionResult Details(int id)
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
        //GET: /Phong/Images
        [HttpGet]
        public IActionResult Images(int id)
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
        [HttpPost]
        public IActionResult UploadImage(int id, IFormFile imageFile)
        {
            
            if (imageFile == null || imageFile.Length == 0)
            {
                return RedirectToAction("Images", new { id });
            }

            //  Tạo đường dẫn thư mục
            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "rooms"
            );
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() +
               Path.GetExtension(imageFile.FileName);

            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            var hinhAnh = new HinhAnhPhong
            {
                MaPhong = id,
                DuongDanAnh = "/images/rooms/" + fileName
            };

            _context.HinhAnhPhongs.Add(hinhAnh);
            _context.SaveChanges();

            return RedirectToAction("Images", new { id });
        }
        //GET: /Phong/DeleteImage
        [HttpGet]
        public IActionResult DeleteImage(int id)
        {
            var hinhAnh = _context.HinhAnhPhongs.Find(id);

            if (hinhAnh == null)
            {
                return NotFound();
            }

            return View(hinhAnh);
        }
        //POST: /Phong/DeleteImage
        [HttpPost, ActionName("DeleteImage")]
        public IActionResult DeleteImageConfirmed(int maHinhAnh)
        {
            var hinhAnh = _context.HinhAnhPhongs.Find(maHinhAnh);

            if (hinhAnh == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(
                _environment.WebRootPath,
                hinhAnh.DuongDanAnh
                    .TrimStart('/')
                    .Replace("/", Path.DirectorySeparatorChar.ToString())
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.HinhAnhPhongs.Remove(hinhAnh);
            _context.SaveChanges();

            return RedirectToAction("Images", new { id = hinhAnh.MaPhong });
        }
    }
}