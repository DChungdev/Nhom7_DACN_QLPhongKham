﻿using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Data.Interfaces;
using QuanLyPhongKham.Models.Entities;
using QuanLyPhongKham.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongKham.Models.Resources;
using QuanLyPhongKham.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace QuanLyPhongKham.Business.Services
{
    public class AppointmentService : BaseService<LichKham>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository) : base(appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        public override async Task<int> AddAsync(LichKham entity)
        {
            var checkData = await CheckDataValidate(entity);
            var existingLichKhams = await _appointmentRepository.GetLichKhamByDateAndTimeAsync((DateTime) entity.NgayKham, entity.GioKham);
            var count = existingLichKhams.Count(lk => lk.TrangThaiLichKham == "Đang xử lý" || lk.TrangThaiLichKham == "Đã đặt");

            // Kiểm tra giới hạn tối đa
            if (count >= 3)
            {
                checkData.Add("NgayKham-GioKham", ResourceVN.Error_GioKhamExist);
            }
            if (checkData.Count > 0)
            {
                throw new ErrorValidDataException(checkData);
            }
            else
            {
                entity.LichKhamId = Guid.NewGuid();
                entity.TrangThaiLichKham = "Đang xử lý";
                int res = await _appointmentRepository.AddAsync(entity);
                if (res > 0)
                {
                    return res;
                }
                else
                {
                    throw new ErrorCreateException();
                }
            }
        }

        public async Task<int> CancelAppointment(Guid id)
        {
            //Lấy ra lịch khám muốn hủy
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            //Lấy ra trạng thái
            string trangThai = appointment.TrangThaiLichKham;
            if (appointment == null)
            {
                throw new ErrorNotFoundException();
            }
            //Chỉ cho hủy khi trạng thái khác hủy và hoàn thành
            if (trangThai.Equals("Đã hủy") || trangThai.Equals("Hoàn thành"))
            {
                throw new ErrorEditException();
            }
            appointment.TrangThaiLichKham = "Đã hủy";
            appointment.NgayCapNhat = DateTime.Now;
            int res = await _appointmentRepository.UpdateAsync(appointment);
            if (res > 0)
            {
                return res;
            }
            else
            {
                throw new ErrorEditException();
            }
        }

        public async Task<int> EditAsync(LichKham lichKham, Guid id)
        {
            //1. Kiểm tra xem có trong CSDL không
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                throw new ErrorNotFoundException();
            }
            //2.Kiểm tra dữ liệu
            var checkData = await CheckDataValidate(lichKham);
            if(appointment.TrangThaiLichKham == "Đã hủy" || appointment.TrangThaiLichKham == "Hoàn thành")
            {
                throw new ErrorEditException();
            }

            if(appointment.NgayKham != lichKham.NgayKham || appointment.GioKham != lichKham.GioKham)
            {
                var appointments = await _appointmentRepository.GetLichKhamByDateAndTimeAsync((DateTime) lichKham.NgayKham, lichKham.GioKham);
                var count = appointments.Count(l => l.TrangThaiLichKham == "Đang xử lý" || l.TrangThaiLichKham == "Đã đặt");
                if (count >= 3)
                {
                    checkData.Add("NgayKham-GioKham", ResourceVN.Error_GioKhamExist);
                }
            }

            if (checkData.Count > 0)
            {
                throw new ErrorValidDataException(checkData);
            }
            else
            {
                //appointment.BenhNhanId = lichKham.BenhNhanId;
                //appointment.BacSiId = lichKham.BacSiId;
                //appointment.NgayKham = lichKham.NgayKham;
                //appointment.GioKham = lichKham.GioKham;
                //appointment.TrangThaiLichKham = "Đang xử lý";
                //appointment.NgayCapNhat = DateTime.Now;
                int res = await _appointmentRepository.UpdateAsync(appointment);
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

        public async Task<IEnumerable<LichKham>> GetAppointmentsByDoctor(Guid bacSiId)
        {
            var lichKhams = await _appointmentRepository.GetAppointmentsByDoctor(bacSiId);
            return lichKhams;
        }

        public async Task<IEnumerable<LichKham>> GetAppointmentsByPatient(Guid benhNhanId)
        {
            var lichKhams = await _appointmentRepository.GetAppointmentsByPatient(benhNhanId);
            return lichKhams;
        }

        public async Task<LichKham>? GetLichKhamLatest(Guid benhNhanId)
        {
            return await _appointmentRepository.GetLichKhamLatest(benhNhanId);
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>
        /// true - không trùng
        /// false - trùng
        /// </returns>
        private bool CheckEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            return false;
        }
        private async Task<Dictionary<string, string>>? CheckDataValidate(LichKham lichKham)
        {
            var errorData = new Dictionary<string, string>();

            //Kiểm tra ngày khám không được nhỏ hơn ngày hiện tại và không được trống
            if (string.IsNullOrEmpty(lichKham.NgayKham.ToString()))
            {
                errorData.Add("NgayKham", ResourceVN.Error_NgayKhamNotEmpty);
            }

            if (lichKham.NgayKham < DateTime.Now)
            {
                errorData.Add("NgayKham", ResourceVN.Error_AOfDateNotLessNow);
            }

            //Kiểm tra giờ khám không được trống
            if (string.IsNullOrEmpty(lichKham.GioKham.ToString()))
            {
                errorData.Add("GioKham", ResourceVN.Error_GioKhamNotEmpty);
            }

            //Kiểm tra bác sĩ được chọn phải tồn tại
            //var bacSi = await _context.BacSis.Where(b => b.BacSiId == lichKham.BacSiId).FirstOrDefaultAsync();

            //if (bacSi == null)
            //{
            //    errorData.Add("BacSiId", ResourceVN.Error_NotFound);
            //}
            //else
            //{
            //    string gioKhams = bacSi.GioLamViec.ToString();
            //    if (string.IsNullOrEmpty(gioKhams))
            //    {
            //        errorData.Add("GioKham", ResourceVN.Error_GioKhamNotExist);
            //    }
            //    else
            //    {
            //        string[] gio = gioKhams.Split(',');
            //        if (gio.Contains(lichKham.GioKham) == false)
            //            errorData.Add("GioKham", ResourceVN.Error_GioKhamOfBacSiNotExist);
            //    }
            //}
            //Kiểm tra thông tin bệnh nhân
            var benhNhan = await _patientRepository.GetByIdAsync(lichKham.BenhNhanId);
            if (benhNhan == null)
            {
                errorData.Add("BenhNhan/BenhNhanId", ResourceVN.Error_NotFound);
            }
            //1.3. Email không được để trống
            if (string.IsNullOrEmpty(lichKham.BenhNhan.Email))
            {
                errorData.Add("BenhNhan/Email", ResourceVN.Error_EmailNotEmpty);
            }
            else
            {
                //Kiểm tra Email đúng định dạng
                if (CheckEmailValid(lichKham.BenhNhan.Email) == false)
                {
                    errorData.Add("BenhNhan/Email", ResourceVN.Error_ValidEmail);
                }
            }
            //2.1. Họ tên không được có số và không được trống
            if (string.IsNullOrEmpty(lichKham.BenhNhan.HoTen))
            {
                errorData.Add("BenhNhan/HoTen", ResourceVN.Error_HoTenNotEmpty);
            }
            if (lichKham.BenhNhan.HoTen.Any(char.IsDigit))
            {
                errorData.Add("BenhNhan/HoTen", ResourceVN.Error_HoTenNotNumber);
            }
            //2.2. Số điện thoại không được có chữ
            if (!string.IsNullOrEmpty(lichKham.BenhNhan.SoDienThoai) && lichKham.BenhNhan.SoDienThoai.Any(char.IsLetter))
            {
                errorData.Add("BenhNhan/SoDienThoai", ResourceVN.Error_PhoneNumberNotLetter);
            }
            //2.3. Ngày sinh không được lớn hơn ngày hiện tại
            if (lichKham.BenhNhan.NgaySinh.HasValue && lichKham.BenhNhan.NgaySinh > DateTime.Now)
            {
                errorData.Add("BenhNhan/NgaySinh", ResourceVN.Error_BOfDateNotGreatNow);
            }

            return errorData;
        }

    }
}