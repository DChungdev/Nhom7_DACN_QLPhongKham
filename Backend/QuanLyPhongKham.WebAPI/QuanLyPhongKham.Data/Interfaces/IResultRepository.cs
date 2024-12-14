using QuanLyPhongKham.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Data.Interfaces
{
    public interface IResultRepository : IBaseRepository<KetQuaKham>
    {
        Dictionary<string, string>? CheckDataValidate(KetQuaKham ketQuaKham);
        Dictionary<string, string>? CheckDataValidateForInsert(KetQuaKham ketQuaKham);
        
        KetQuaKham? GetKetQuaKhamByLichKhamId(Guid lichKhamId);
    }
}
