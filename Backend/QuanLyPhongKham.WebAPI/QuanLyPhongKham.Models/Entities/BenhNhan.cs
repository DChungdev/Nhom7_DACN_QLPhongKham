using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class BenhNhan
    {
        public Guid BenhNhanId { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string TienSuBenhLy { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    }
}
