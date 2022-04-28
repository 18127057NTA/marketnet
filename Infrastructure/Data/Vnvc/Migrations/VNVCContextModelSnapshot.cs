﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Vnvc.Migrations
{
    [DbContext(typeof(VNVCContext))]
    partial class VNVCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Core.Entities.VNVCModels.ChiNhanh", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ten")
                        .HasColumnType("TEXT");

                    b.Property<int>("TinhThanhId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TinhThanhId");

                    b.ToTable("chiNhanh");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.ChiTietDonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DonGia")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DonHangId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaSanPham")
                        .HasColumnType("TEXT");

                    b.Property<int>("SoLuong")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TenSanPham")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DonHangId");

                    b.ToTable("chiTietDonHang");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChiNhanhId")
                        .HasColumnType("TEXT");

                    b.Property<int>("KhachHangId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("NgayMua")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NgayTiem")
                        .HasColumnType("TEXT");

                    b.Property<string>("TinhTrang")
                        .HasColumnType("TEXT");

                    b.Property<int>("TongTien")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ChiNhanhId");

                    b.HasIndex("KhachHangId");

                    b.ToTable("donHang");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.KhachHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cccd")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("HoTen")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sdt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("khachHang");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.TinhThanh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ten")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tinhThanh");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.Vip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("KhachHangId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("KhachHangMaVip")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NgayBD")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NgayKT")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("KhachHangId");

                    b.ToTable("vip");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.ChiNhanh", b =>
                {
                    b.HasOne("Core.Entities.VNVCModels.TinhThanh", "TinhThanh")
                        .WithMany()
                        .HasForeignKey("TinhThanhId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TinhThanh");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.ChiTietDonHang", b =>
                {
                    b.HasOne("Core.Entities.VNVCModels.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("DonHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.DonHang", b =>
                {
                    b.HasOne("Core.Entities.VNVCModels.ChiNhanh", "ChiNhanh")
                        .WithMany()
                        .HasForeignKey("ChiNhanhId");

                    b.HasOne("Core.Entities.VNVCModels.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("KhachHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiNhanh");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("Core.Entities.VNVCModels.Vip", b =>
                {
                    b.HasOne("Core.Entities.VNVCModels.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("KhachHangId");

                    b.Navigation("KhachHang");
                });
#pragma warning restore 612, 618
        }
    }
}
