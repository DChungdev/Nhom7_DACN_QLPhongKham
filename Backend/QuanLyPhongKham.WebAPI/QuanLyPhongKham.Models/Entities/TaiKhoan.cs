using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class TaiKhoan
    {
        public Guid TaiKhoanId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } 
        public Guid BenhNhanId { get; set; }
        public Guid BacSiId { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        public BenhNhan BenhNhan { get; set; }
        public BacSi BacSi { get; set; }
    }
}
