using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class LichKham_Thuoc
    {
        public Guid LichKhamId { get; set; }
        public Guid ThuocId { get; set; }
        public int SoLuongThuoc { get; set; }
        public decimal ChiPhi { get; set; }
        public LichKham LichKham { get; set; }
        public Thuoc Thuoc { get; set; }
    }
}
