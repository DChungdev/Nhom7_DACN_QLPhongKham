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

        public Task<int> AddAsync(BenhNhan entity)
        {
            throw new NotImplementedException();
        }

        // Kiểm tra dữ liệu hợp lệ khi sửa hoặc thêm dịch vụ
        public Dictionary<string, string>? CheckDataValidate(KetQuaKham ketQuaKham)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(ketQuaKham.ChanDoan))
                errors.Add("ChanDoan", "Chẩn đoán không được để trống");

            return errors;
        }

        public Dictionary<string, string>? CheckDataValidate(BenhNhan benhNhan)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string>? CheckDataValidateForInsert(KetQuaKham ketQuaKham)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(ketQuaKham.ChanDoan))
                errors.Add("ChanDoan", "Chẩn đoán không được để trống");
           
            return errors;
        }

        public Dictionary<string, string>? CheckDataValidateForInsert(BenhNhan benhNhan)
        {
            throw new NotImplementedException();
        }

        public string GetNextMaKetQuaKham()
        {
            // Lấy danh sách mã kết quả khám
            var maxMaKK = _context.KetQuaKhams
                .AsEnumerable() // Chuyển sang client side
                .Select(kk => new
                {
                    MaKK = kk.KetQuaKhamId.ToString(), // Chuyển Guid thành chuỗi
                    So = int.Parse(kk.KetQuaKhamId.ToString().Substring(2)) // Tách phần số, bỏ qua 2 ký tự đầu tiên "KK"
                })
                .OrderByDescending(kk => kk.So) // Sắp xếp theo số, giảm dần
                .FirstOrDefault();

            // Nếu không có kết quả khám nào trong hệ thống, mã bắt đầu từ "KK001"
            if (maxMaKK == null)
            {
                return "KK001";
            }

            // Lấy phần số lớn nhất và cộng thêm 1
            int nextSo = maxMaKK.So + 1;

            // Kết hợp phần chữ "KK" và phần số (cộng thêm 1), định dạng phần số với 3 chữ số
            return $"KK{nextSo:D3}";
        }

        public Task<int> UpdateAsync(BenhNhan entity)
        {
            throw new NotImplementedException();
        }

        public async Task<KetQuaKham> GetByIdAsync(Guid ketQuaKhamId)
        {
            // Sử dụng DbContext để lấy thông tin KetQuaKham
            return await _context.KetQuaKhams.FirstOrDefaultAsync(k => k.KetQuaKhamId == ketQuaKhamId);
        }

    }
}
