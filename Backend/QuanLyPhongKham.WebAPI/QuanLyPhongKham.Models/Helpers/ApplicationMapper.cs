﻿using AutoMapper;
using QuanLyPhongKham.Models.Entities;
using QuanLyPhongKham.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<BenhNhan, BenhNhanModel>().ReverseMap();
        }
    }
}