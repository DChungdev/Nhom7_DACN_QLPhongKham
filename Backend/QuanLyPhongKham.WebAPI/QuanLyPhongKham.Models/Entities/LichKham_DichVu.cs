using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Entities
{
    public class LichKham_DichVu
    {
        public Guid LichKhamId { get; set; }
        public Guid DichVuId { get; set; }
        public int SoLuongDichVu { get; set; }
        public decimal ChiPhi { get; set; }
        public LichKham LichKham { get; set; }
        public DichVu DichVu { get; set; }
    }
}
