﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyPhongKham.Data.Context;

#nullable disable

namespace QuanLyPhongKham.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241105093201_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BacSi", b =>
                {
                    b.Property<Guid>("BacSiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BangCap")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioLamViec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("KhoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaBacSi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoNamKinhNghiem")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BacSiId");

                    b.HasIndex("KhoaId");

                    b.HasIndex("UserId");

                    b.ToTable("BacSis");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BacSiDichVu", b =>
                {
                    b.Property<Guid>("BacSiId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DichVuId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BacSiId", "DichVuId");

                    b.HasIndex("DichVuId");

                    b.ToTable("BacSiDichVu");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BenhNhan", b =>
                {
                    b.Property<Guid>("BenhNhanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoaiGioiTinh")
                        .HasColumnType("int");

                    b.Property<string>("MaBenhNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TienSuBenhLy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BenhNhanId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("BenhNhans");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.DanhGiaDichVu", b =>
                {
                    b.Property<Guid>("DanhGiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BacSiId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BenhNhanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("DanhGia")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhanHoi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DanhGiaId");

                    b.HasIndex("BacSiId");

                    b.HasIndex("BenhNhanId");

                    b.ToTable("DanhGiaDichVus");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.DichVu", b =>
                {
                    b.Property<Guid>("DichVuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("KhoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaDichVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTaDichVu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDichVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DichVuId");

                    b.HasIndex("KhoaId");

                    b.ToTable("DichVus");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.KetQuaKham", b =>
                {
                    b.Property<Guid>("KetQuaKhamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChanDoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChiDinhThuoc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LichKhamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.HasKey("KetQuaKhamId");

                    b.HasIndex("LichKhamId")
                        .IsUnique()
                        .HasFilter("[LichKhamId] IS NOT NULL");

                    b.ToTable("KetQuaKhams");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.Khoa", b =>
                {
                    b.Property<Guid>("KhoaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MaKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KhoaId");

                    b.ToTable("Khoas");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.LichKham", b =>
                {
                    b.Property<Guid>("LichKhamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacSiId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BenhNhanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GioKham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKham")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrangThaiLichKham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LichKhamId");

                    b.HasIndex("BacSiId");

                    b.HasIndex("BenhNhanId");

                    b.ToTable("LichKhams");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BacSi", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.Khoa", "Khoa")
                        .WithMany("BacSis")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Khoa");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BacSiDichVu", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.BacSi", "BacSi")
                        .WithMany("BacSiDichVus")
                        .HasForeignKey("BacSiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhongKham.Models.Entities.DichVu", "DichVu")
                        .WithMany("BacSiDichVus")
                        .HasForeignKey("DichVuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BacSi");

                    b.Navigation("DichVu");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BenhNhan", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("QuanLyPhongKham.Models.Entities.BenhNhan", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.DanhGiaDichVu", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.BacSi", "BacSi")
                        .WithMany("DanhGiaDichVus")
                        .HasForeignKey("BacSiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QuanLyPhongKham.Models.Entities.BenhNhan", "BenhNhan")
                        .WithMany("DanhGiaDichVus")
                        .HasForeignKey("BenhNhanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BacSi");

                    b.Navigation("BenhNhan");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.DichVu", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.Khoa", "Khoa")
                        .WithMany("DichVus")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.KetQuaKham", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.LichKham", "LichKham")
                        .WithOne("KetQuaKham")
                        .HasForeignKey("QuanLyPhongKham.Models.Entities.KetQuaKham", "LichKhamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("LichKham");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.LichKham", b =>
                {
                    b.HasOne("QuanLyPhongKham.Models.Entities.BacSi", "BacSi")
                        .WithMany("LichKhams")
                        .HasForeignKey("BacSiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhongKham.Models.Entities.BenhNhan", "BenhNhan")
                        .WithMany("LichKhams")
                        .HasForeignKey("BenhNhanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BacSi");

                    b.Navigation("BenhNhan");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BacSi", b =>
                {
                    b.Navigation("BacSiDichVus");

                    b.Navigation("DanhGiaDichVus");

                    b.Navigation("LichKhams");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.BenhNhan", b =>
                {
                    b.Navigation("DanhGiaDichVus");

                    b.Navigation("LichKhams");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.DichVu", b =>
                {
                    b.Navigation("BacSiDichVus");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.Khoa", b =>
                {
                    b.Navigation("BacSis");

                    b.Navigation("DichVus");
                });

            modelBuilder.Entity("QuanLyPhongKham.Models.Entities.LichKham", b =>
                {
                    b.Navigation("KetQuaKham");
                });
#pragma warning restore 612, 618
        }
    }
}