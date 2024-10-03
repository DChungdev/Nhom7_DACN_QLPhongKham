using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class ChamSocKhachHang
    {
        public Guid ChamSocId { get; set; }
        public Guid BenhNhanId { get; set; }
        public string DanhGiaDichVu { get; set; }
        public string PhanHoi { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        public BenhNhan BenhNhan { get; set; }
    }
}
