using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<BacSi> BacSis { get; set; }
        public DbSet<Thuoc> Thuocs { get; set; }
        public DbSet<VatTu> VatTus { get; set; }
        public DbSet<LichKham> LichKhams { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<LichKham_DichVu> LichKham_DichVus { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<DanhGiaDichVu> DanhGiaDichVus { get; set; }
        public DbSet<LichKham_Thuoc> LichKham_Thuocs { get; set; }
        public DbSet<KetQuaKham> KetQuaKhams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Mối quan hệ giữa BenhNhan và ApplicationUser
            //modelBuilder.Entity<BenhNhan>()
            // .HasOne<ApplicationUser>()
            // .WithOne() 
            // .HasForeignKey<BenhNhan>(b => b.UserId) 
            // .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<BenhNhan>()
                .HasOne(b => b.User) // Liên kết đến ApplicationUser
                .WithOne() // Một BenhNhan chỉ có một ApplicationUser
                .HasForeignKey<BenhNhan>(b => b.UserId) // Sử dụng UserId làm khóa ngoại
                .OnDelete(DeleteBehavior.SetNull);

            // Khóa chính cho DanhGiaDichVu
            modelBuilder.Entity<DanhGiaDichVu>()
                .HasKey(d => d.DanhGiaId);

            // Mối quan hệ giữa BenhNhan và LichKham
            modelBuilder.Entity<BenhNhan>()
                .HasMany(b => b.LichKhams)
                .WithOne(l => l.BenhNhan)
                .HasForeignKey(l => l.BenhNhanId)
                .OnDelete(DeleteBehavior.Restrict); // Đổi thành Restrict

            modelBuilder.Entity<BenhNhan>()
                .HasMany(b => b.HoaDons)
                .WithOne(h => h.BenhNhan)
                .HasForeignKey(h => h.BenhNhanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ giữa BacSi và LichKham
            modelBuilder.Entity<BacSi>()
                .HasMany(b => b.LichKhams)
                .WithOne(l => l.BacSi)
                .HasForeignKey(l => l.BacSiId)
                .OnDelete(DeleteBehavior.Cascade); // Giữ nguyên

            // Mối quan hệ giữa LichKham và KetQuaKham
            modelBuilder.Entity<LichKham>()
                .HasOne(l => l.KetQuaKham)
                .WithOne(kk => kk.LichKham)
                .HasForeignKey<KetQuaKham>(kk => kk.LichKhamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DichVu>()
                .Property(d => d.DonGia)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<HoaDon>()
                .Property(h => h.TongTien)
                .HasColumnType("decimal(18,2)");

            // Mối quan hệ giữa LichKham và DichVu
            modelBuilder.Entity<LichKham_DichVu>()
                .HasKey(ld => new { ld.LichKhamId, ld.DichVuId });

            modelBuilder.Entity<LichKham_DichVu>()
                .HasOne(ld => ld.LichKham)
                .WithMany(l => l.LichKhamDichVus)
                .HasForeignKey(ld => ld.LichKhamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LichKham_DichVu>()
                .HasOne(ld => ld.DichVu)
                .WithMany(d => d.LichKhamDichVus)
                .HasForeignKey(ld => ld.DichVuId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ giữa LichKham và Thuoc
            modelBuilder.Entity<LichKham_Thuoc>()
                .HasKey(lt => new { lt.LichKhamId, lt.ThuocId });

            modelBuilder.Entity<LichKham_Thuoc>()
                .HasOne(lt => lt.LichKham)
                .WithMany(l => l.LichKhamThuocs)
                .HasForeignKey(lt => lt.LichKhamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LichKham_Thuoc>()
                .HasOne(lt => lt.Thuoc)
                .WithMany(t => t.LichKhamThuocs)
                .HasForeignKey(lt => lt.ThuocId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ giữa BenhNhan và DanhGiaDichVu
            modelBuilder.Entity<BenhNhan>()
                .HasMany(b => b.DanhGiaDichVus)
                .WithOne(d => d.BenhNhan)
                .HasForeignKey(d => d.BenhNhanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ giữa LichKham và HoaDon
            modelBuilder.Entity<LichKham>()
                .HasOne(l => l.HoaDon)
                .WithOne(h => h.LichKham)
                .HasForeignKey<HoaDon>(h => h.LichKhamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Định dạng chi phí
            modelBuilder.Entity<LichKham_DichVu>()
                .Property(l => l.ChiPhi)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<LichKham_Thuoc>()
                .Property(l => l.ChiPhi)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Thuoc>()
                .Property(t => t.DonGia)
                .HasColumnType("decimal(18,2)");
        }
       }
}
