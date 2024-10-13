using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class HoaDon
    {
        public Guid HoaDonId { get; set; }
        public Guid BenhNhanId { get; set; }
        public Guid LichKhamId { get; set; }
        public DateTime? NgayLapHoaDon { get; set; }
        public decimal TongTien { get; set; }
        public string TinhTrangThanhToan { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
        public BenhNhan BenhNhan { get; set; }
        public LichKham LichKham { get; set; }
    }
}