using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Models
{
    public class DoctorAppointmentCountModel
    {
        public Guid BacSiId { get; set; }
        public string HoTen { get; set; }
        public int AppointmentCount { get; set; }
    }
}
