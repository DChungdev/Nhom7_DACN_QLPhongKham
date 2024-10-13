using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class DichVu
    {
        public Guid DichVuId { get; set; }
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string MoTaDichVu { get; set; }
        public decimal DonGia { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<LichKham_DichVu>? LichKhamDichVus { get; set; } // Quan hệ N:M với LichKham
    }
}
