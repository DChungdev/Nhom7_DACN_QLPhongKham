﻿using QuanLyPhongKham.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Business.Interfaces
{
    public interface IResultService:IBaseService<KetQuaKham>
    {
        Task<IEnumerable<KetQuaKham>> GetAllByLichKhamIdAsync(Guid lichKhamId);

    }
}