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

            //Kiểm tra LichKhamId truyền vào có tồn tại hay không
            var lk = _context.LichKhams.Where(lk => lk.LichKhamId == ketQuaKham.LichKhamId).FirstOrDefault();
            if (lk == null) 
            { 
                errors.Add("LichKhamId", "Lich kham khong ton tai!"); 
            }
            else if(lk.TrangThaiLichKham != "Đã đặt")
            {
                errors.Add("LichKham/TrangThai", "Khong the tao ket qua cho lich kham nay!");
            }
            var kq = _context.KetQuaKhams.Where(kq => kq.LichKhamId == ketQuaKham.LichKhamId).FirstOrDefault();
            if(kq != null)
            {
                errors.Add("LichKhamId", "Lich kham nay da co ket qua");
            }
            return errors;
        }

        public Dictionary<string, string>? CheckDataValidateForInsert(KetQuaKham ketQuaKham)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(ketQuaKham.ChanDoan))
                errors.Add("ChanDoan", "Chẩn đoán không được để trống");

            return errors;
        }

        public KetQuaKham? GetKetQuaKhamByLichKhamId(Guid lichKhamId)
        {
            var kq =  _context.KetQuaKhams.Where(k=>k.LichKhamId == lichKhamId).FirstOrDefault();
            return kq;
        }
    }
}
