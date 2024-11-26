using QuanLyPhongKham.Data.Context;
using QuanLyPhongKham.Data.Interfaces;
using QuanLyPhongKham.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Data.Repositories
{
    public class ResultRepository : BaseRepository<KetQuaKham>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Kiểm tra dữ liệu hợp lệ khi sửa hoặc thêm dịch vụ
        public Dictionary<string, string>? CheckDataValidate(KetQuaKham ketQuaKham)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(ketQuaKham.ChanDoan))
                errors.Add("ChanDoan", "Chẩn đoán không được để trống");

            return errors;
        }

        public Dictionary<string, string>? CheckDataValidateForInsert(KetQuaKham ketQuaKham)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(ketQuaKham.ChanDoan))
                errors.Add("ChanDoan", "Chẩn đoán không được để trống");
           
            return errors;
        }

    }
}
