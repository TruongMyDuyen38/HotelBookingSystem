using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiPhongs",
                columns: table => new
                {
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GiaCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhongs", x => x.MaLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.MaVaiTro);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false),
                    GiaMotDem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.MaPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_LoaiPhongs_MaLoaiPhong",
                        column: x => x.MaLoaiPhong,
                        principalTable: "LoaiPhongs",
                        principalColumn: "MaLoaiPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaVaiTro = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_VaiTros_MaVaiTro",
                        column: x => x.MaVaiTro,
                        principalTable: "VaiTros",
                        principalColumn: "MaVaiTro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhPhongs",
                columns: table => new
                {
                    MaHinhAnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    DuongDanAnh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnhPhongs", x => x.MaHinhAnh);
                    table.ForeignKey(
                        name: "FK_HinhAnhPhongs_Phongs_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Phongs",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHangs_TaiKhoans_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatPhongs",
                columns: table => new
                {
                    MaDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhongs", x => x.MaDatPhong);
                    table.ForeignKey(
                        name: "FK_DatPhongs_KhachHangs_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDatPhongs",
                columns: table => new
                {
                    MaChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    NgayNhanPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTraPhong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoNguoi = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDatPhongs", x => x.MaChiTiet);
                    table.ForeignKey(
                        name: "FK_ChiTietDatPhongs_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDatPhongs_Phongs_MaPhong",
                        column: x => x.MaPhong,
                        principalTable: "Phongs",
                        principalColumn: "MaPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToans",
                columns: table => new
                {
                    MaThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToans", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToans_DatPhongs_MaDatPhong",
                        column: x => x.MaDatPhong,
                        principalTable: "DatPhongs",
                        principalColumn: "MaDatPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDatPhongs_MaDatPhong",
                table: "ChiTietDatPhongs",
                column: "MaDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDatPhongs_MaPhong",
                table: "ChiTietDatPhongs",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DatPhongs_MaKhachHang",
                table: "DatPhongs",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhPhongs_MaPhong",
                table: "HinhAnhPhongs",
                column: "MaPhong");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaTaiKhoan",
                table: "KhachHangs",
                column: "MaTaiKhoan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_MaLoaiPhong",
                table: "Phongs",
                column: "MaLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_MaVaiTro",
                table: "TaiKhoans",
                column: "MaVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_MaDatPhong",
                table: "ThanhToans",
                column: "MaDatPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDatPhongs");

            migrationBuilder.DropTable(
                name: "HinhAnhPhongs");

            migrationBuilder.DropTable(
                name: "ThanhToans");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "DatPhongs");

            migrationBuilder.DropTable(
                name: "LoaiPhongs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "VaiTros");
        }
    }
}
