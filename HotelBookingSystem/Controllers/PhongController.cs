using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    public class PhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhongController(ApplicationDbContext context)
        {
            _context = context;
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

            return View(phong);
        }
    }
}