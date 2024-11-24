﻿using QuanLyPhongKham.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Models.Models
{
	public class DichVuModel
	{
		public Guid DichVuId { get; set; }
		public Guid? KhoaId { get; set; }
		public string MaDichVu { get; set; }
		public string TenDichVu { get; set; }
		public string? MoTaDichVu { get; set; }
		public decimal DonGia { get; set; }
		public DateTime? NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

	}
}