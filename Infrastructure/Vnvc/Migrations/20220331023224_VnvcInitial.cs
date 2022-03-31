using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Vnvc.Migrations
{
    public partial class VnvcInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "khachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KH_ID = table.Column<string>(type: "nvarchar", nullable: true),
                    KH_CCCD = table.Column<string>(type: "nvarchar", nullable: true),
                    KH_HOTEN = table.Column<string>(type: "nvarchar", nullable: true),
                    KH_EMAIL = table.Column<string>(type: "nvarchar", nullable: true),
                    KH_SDT = table.Column<string>(type: "nvarchar", nullable: true),
                    KH_DIACHI = table.Column<string>(type: "nvarchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tinhThanh",
                columns: table => new
                {
                    TT_ID = table.Column<string>(type: "nvarchar", nullable: false),
                    TT_TENTT = table.Column<string>(type: "nvarchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinhThanh", x => x.TT_ID);
                });

            migrationBuilder.CreateTable(
                name: "vips",
                columns: table => new
                {
                    VIP_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VIP_IDKH = table.Column<int>(type: "INTEGER", nullable: false),
                    VIP_NGAYBD = table.Column<DateTime>(type: "datetime", nullable: true),
                    VIP_NGAYKT = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vips", x => x.VIP_ID);
                    table.ForeignKey(
                        name: "FK_vips_khachHang_KHACH_HANGId",
                        column: x => x.VIP_IDKH,
                        principalTable: "khachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "chiNhanh",
                columns: table => new
                {
                    CN_ID = table.Column<string>(type: "nvarchar", nullable: false),
                    CN_IDTINH = table.Column<string>(type: "nvarchar", nullable: false),
                    CN_TENCN = table.Column<string>(type: "nvarchar", nullable: true),
                    CN_DIACHI = table.Column<string>(type: "nvarchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiNhanh", x => x.CN_ID);
                    table.ForeignKey(
                        name: "FK_chiNhanh_tinhThanh_TINH_THANHTT_ID",
                        column: x => x.CN_IDTINH,
                        principalTable: "tinhThanh",
                        principalColumn: "TT_ID");
                });

            migrationBuilder.CreateTable(
                name: "donHang",
                columns: table => new
                {
                    DH_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DH_IDCN = table.Column<string>(type: "nvarchar", nullable: false),
                    DH_IDKH = table.Column<int>(type: "INTEGER", nullable: false),
                    DH_NGAY = table.Column<DateTime>(type: "datetime", nullable: true),
                    DH_NGAYTIEM = table.Column<DateTime>(type: "datetime", nullable: true),
                    DH_TONGTIEN = table.Column<int>(type: "INTEGER", nullable: false),
                    DH_TTRANG = table.Column<string>(type: "nvarchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donHang", x => x.DH_ID);
                    table.ForeignKey(
                        name: "FK_donHang_chiNhanh_CHI_NHANHCN_ID",
                        column: x => x.DH_IDCN,
                        principalTable: "chiNhanh",
                        principalColumn: "CN_ID");
                    table.ForeignKey(
                        name: "FK_donHang_khachHang_KHACH_HANGId",
                        column: x => x.DH_IDKH,
                        principalTable: "khachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cTietDonHang",
                columns: table => new
                {
                    CTDH_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CTDH_IDDH = table.Column<int>(type: "INTEGER", nullable: false),
                    CTDH_IDSP = table.Column<string>(type: "nvarchar", nullable: false),
                    CTDH_TENSP = table.Column<string>(type: "nvarchar", nullable: true),
                    CTDH_GIA = table.Column<int>(type: "INTEGER", nullable: false),
                    CTDH_SL = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cTietDonHang", x => x.CTDH_ID);
                    table.ForeignKey(
                        name: "FK_cTietDonHang_donHang_DON_HANGDH_ID",
                        column: x => x.CTDH_IDDH,
                        principalTable: "donHang",
                        principalColumn: "DH_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiNhanh_TINH_THANHCN_IDTINH",
                table: "chiNhanh",
                column: "CN_IDTINH");

            migrationBuilder.CreateIndex(
                name: "IX_cTietDonHang_DON_HANGCTDH_IDDH",
                table: "cTietDonHang",
                column: "CTDH_IDDH");

            migrationBuilder.CreateIndex(
                name: "IX_donHang_CHI_NHANHDH_IDCN",
                table: "donHang",
                column: "DH_IDCN");

            migrationBuilder.CreateIndex(
                name: "IX_donHang_KHACH_HANGDH_IDKH",
                table: "donHang",
                column: "DH_IDKH");

            migrationBuilder.CreateIndex(
                name: "IX_vips_KHACH_HANGVIP_IDKH",
                table: "vips",
                column: "VIP_IDKH");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cTietDonHang");

            migrationBuilder.DropTable(
                name: "vips");

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
