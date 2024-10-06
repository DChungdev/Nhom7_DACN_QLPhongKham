using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class BacSi
    {
        public Guid BacSiId { get; set; }
        public string MaBacSi { get; set; }
        public string HoTen { get; set; }
        public string ChuyenKhoa { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }
        public int? SoNamKinhNghiem { get; set; }
        public string GioLamViec { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        // Liên kết đến aspnetusers
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<LichKham> LichKhams { get; set; }
    }
}
