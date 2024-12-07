using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Data.Interfaces;
using QuanLyPhongKham.Models.Entities;

namespace QuanLyPhongKham.Business.Services
{
    public class DepartmentService : BaseService<Khoa>, IDepartmentService
    {
        public DepartmentService(IBaseRepository<Khoa> repository) : base(repository)
        {
        }
    }
}
