using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Vnvc.Migrations
{
    public partial class VnvcInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "khachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cccd = table.Column<string>(type: "TEXT", nullable: true),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    HoTen = table.Column<string>(type: "TEXT", nullable: true),
                    Sdt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tinhThanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ten = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinhThanh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KhachHangId = table.Column<int>(type: "INTEGER", nullable: true),
                    KhachHangMaVip = table.Column<string>(type: "TEXT", nullable: true),
                    NgayBD = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NgayKT = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vip_khachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "khachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "chiNhanh",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: true),
                    Ten = table.Column<string>(type: "TEXT", nullable: true),
                    TinhThanhId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiNhanh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiNhanh_tinhThanh_TinhThanhId",
                        column: x => x.TinhThanhId,
                        principalTable: "tinhThanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChiNhanhId = table.Column<string>(type: "TEXT", nullable: true),
                    KhachHangId = table.Column<int>(type: "INTEGER", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NgayTiem = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TongTien = table.Column<int>(type: "INTEGER", nullable: false),
                    TinhTrang = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_donHang_chiNhanh_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "chiNhanh",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_donHang_khachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "khachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietDonHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DonGia = table.Column<int>(type: "INTEGER", nullable: false),
                    DonHangId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaSanPham = table.Column<string>(type: "TEXT", nullable: true),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false),
                    TenSanPham = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietDonHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietDonHang_donHang_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "donHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiNhanh_TinhThanhId",
                table: "chiNhanh",
                column: "TinhThanhId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDonHang_DonHangId",
                table: "chiTietDonHang",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_donHang_ChiNhanhId",
                table: "donHang",
                column: "ChiNhanhId");

            migrationBuilder.CreateIndex(
                name: "IX_donHang_KhachHangId",
                table: "donHang",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_vip_KhachHangId",
                table: "vip",
                column: "KhachHangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietDonHang");

            migrationBuilder.DropTable(
                name: "vip");

            migrationBuilder.DropTable(
                name: "donHang");

            migrationBuilder.DropTable(
                name: "chiNhanh");

            migrationBuilder.DropTable(
                name: "khachHang");

            migrationBuilder.DropTable(
                name: "tinhThanh");
        }
    }
}
