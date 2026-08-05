using HotelBookingSystem.Data;
using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Controllers
{
    public class LoaiPhongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiPhongController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dsLoaiPhong = _context.LoaiPhongs.ToList();

            return View(dsLoaiPhong);
        }
        //GET: /LoaiPhong/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //POST: /LoaiPhong/Create
        [HttpPost]
        public IActionResult Create(LoaiPhong model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.LoaiPhongs.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: /LoaiPhong/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var loaiPhong = _context.LoaiPhongs.Find(id);
            if (loaiPhong == null)
            {
                return NotFound();
            }
            return View(loaiPhong);
        }
        //POST: /LoaiPhong/Edit
        [HttpPost]
        public IActionResult Edit(LoaiPhong model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.LoaiPhongs.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET: /LoaiPhong/Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var loaiPhong = _context.LoaiPhongs.Find(id);
            if (loaiPhong == null)
            {
                return NotFound();
            }
            return View(loaiPhong);
        }
        //POST: /LoaiPhong/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var loaiPhong = _context.LoaiPhongs.Find(id);

            if (loaiPhong == null)
            {
                return NotFound();
            }

            _context.LoaiPhongs.Remove(loaiPhong);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}