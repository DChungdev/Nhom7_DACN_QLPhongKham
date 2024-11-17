using QuanLyPhongKham.Models.Models;
using System;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
	public class KhoaModel
	{
		public Guid KhoaId { get; set; }  // ID của khoa
		public string MaKhoa { get; set; }  // Mã khoa (mã duy nhất)
		public string TenKhoa { get; set; }  // Tên khoa
		public DateTime? NgayTao { get; set; } = DateTime.Now;  // Ngày tạo khoa
		public DateTime? NgayCapNhat { get; set; } = DateTime.Now;  // Ngày cập nhật khoa

		// Nếu cần trả về danh sách các dịch vụ thuộc khoa này
		public virtual ICollection<DichVuModel>? DichVus { get; set; }

		// Nếu cần trả về danh sách bác sĩ thuộc khoa này
	}
}
