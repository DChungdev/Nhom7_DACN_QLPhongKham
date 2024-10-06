using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class LichKham
    {
        public Guid LichKhamId { get; set; }
        public Guid BenhNhanId { get; set; }
        public Guid BacSiId { get; set; }
        public DateTime? NgayKham { get; set; }
        public string GioKham { get; set; }
        public string TrangThaiLichKham { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public BenhNhan BenhNhan { get; set; }
        public BacSi BacSi { get; set; }
        public virtual HoaDon HoaDon { get; set; }     // Quan hệ 1:1 với HoaDon
        public virtual KetQuaKham KetQuaKham { get; set; } // Mối quan hệ với KetQuaKham
        public virtual ICollection<LichKham_DichVu> LichKhamDichVus { get; set; } // Quan hệ N:M với DichVu
        public virtual ICollection<LichKham_Thuoc> LichKhamThuocs { get; set; }   // Quan hệ N:M với Thuoc
    }
}
