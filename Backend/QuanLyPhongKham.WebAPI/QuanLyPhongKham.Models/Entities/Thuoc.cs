using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class Thuoc
    {
        public Guid ThuocId { get; set; }
        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string LoaiThuoc { get; set; }
        public string DangThuoc { get; set; }
        public int? SoLuong { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public decimal DonGia { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<LichKham_Thuoc> LichKhamThuocs { get; set; }
    }
}
