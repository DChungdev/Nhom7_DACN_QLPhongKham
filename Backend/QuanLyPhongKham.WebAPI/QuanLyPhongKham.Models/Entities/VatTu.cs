using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class VatTu
    {
        public Guid VatTuId { get; set; }
        public string TenVatTu { get; set; }
        public string LoaiVatTu { get; set; }
        public int? SoLuong { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
    }
}
