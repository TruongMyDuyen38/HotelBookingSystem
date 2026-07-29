using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace HotelBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<HinhAnhPhong> HinhAnhPhongs { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<ChiTietDatPhong> ChiTietDatPhongs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

    }
    
}
