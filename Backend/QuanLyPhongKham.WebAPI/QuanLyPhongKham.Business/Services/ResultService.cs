using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Data.Context;
using QuanLyPhongKham.Data.Interfaces;
using QuanLyPhongKham.Models.Entities;
using QuanLyPhongKham.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Business.Services
{
    public class ResultService : BaseService<KetQuaKham>, IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository) : base(resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public override async Task<int> AddAsync(KetQuaKham entity)
        {
            // Tạo ID mới cho `KetQuaKham`
            entity.KetQuaKhamId = Guid.NewGuid();
            entity.NgayTao = DateTime.Now;
            entity.NgayCapNhat = DateTime.Now;

            // Kiểm tra dữ liệu hợp lệ khi thêm mới
            var checkData = _resultRepository.CheckDataValidateForInsert(entity);

            // Nếu có lỗi dữ liệu, ném ngoại lệ
            if (checkData != null && checkData.Count > 0)
            {
                throw new ErrorValidDataException(checkData);
            }

            // Thực hiện thêm mới entity vào cơ sở dữ liệu
            int res = await _resultRepository.AddAsync(entity);

            // Kiểm tra kết quả thêm mới
            if (res > 0)
            {
                return res; // Trả về kết quả nếu thành công
            }
            else
            {
                throw new ErrorCreateException(); // Ném ngoại lệ nếu tạo thất bại
            }
        }

        public Task<IEnumerable<KetQuaKham>> GetAllByLichKhamIdAsync(Guid lichKhamId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNextMaKetQuaKhamAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<int> UpdateAsync(KetQuaKham entity)
        {
            var checkData = _resultRepository.CheckDataValidate(entity);

            //Nếu hợp lệ thì cho thêm, không thì lỗi
            if (checkData.Count > 0)
            {
                throw new ErrorValidDataException(checkData);
            }
            else
            {
                int res = await _resultRepository.UpdateAsync(entity);

                if (res > 0)
                {
                    return res;
                }
                else
                {
                    throw new ErrorEditException();
                }
            }
        }

        public async Task<KetQuaKham> GetByIdAsync(Guid ketQuaKhamId)
        {
            // Gọi repository để lấy thông tin KetQuaKham theo Id
            return await _resultRepository.GetByIdAsync(ketQuaKhamId);
        }
    }
}
