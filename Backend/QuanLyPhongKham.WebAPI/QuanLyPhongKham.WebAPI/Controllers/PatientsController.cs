using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Models.Entities;
using QuanLyPhongKham.Models.Exceptions;
using QuanLyPhongKham.Models.Models;

namespace QuanLyPhongKham.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;


        public PatientsController(IPatientService patientService, IMapper mapper, IAuthService authService)
        {
            _patientService = patientService;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPatient() 
        { 
            var patients = await _patientService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BenhNhanModel>>(patients));
        }

        [HttpGet("{benhNhanId}")]
        public async Task<IActionResult> GetPatientById(Guid benhNhanId)
        {
            //Lấy dữ liệu
            var employeeById = await _patientService.GetByIdAsync(benhNhanId);
            return Ok(_mapper.Map<BenhNhanModel>(employeeById));
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] BenhNhanModel benhNhan)
        {
            int res = await _patientService.AddAsync(_mapper.Map<BenhNhan>(benhNhan));
            return StatusCode(201, res);
        }

        [HttpPut("{benhNhanId}")]
        public async Task<IActionResult> UpdatePatient(Guid benhNhanId, [FromBody] BenhNhanModel benhNhan)
        {
            if(benhNhanId != benhNhan.BenhNhanId)
            {
                return BadRequest("Id không giống!");
            }
            var existingBN = await _patientService.GetByIdAsync(benhNhanId);
            existingBN.HoTen = benhNhan.HoTen;
            existingBN.NgaySinh = benhNhan.NgaySinh;
            existingBN.SoDienThoai = benhNhan.SoDienThoai;
            existingBN.Email = benhNhan.Email;
            existingBN.DiaChi = benhNhan.DiaChi;
            existingBN.TienSuBenhLy = benhNhan.TienSuBenhLy;
            int res = await _patientService.UpdateAsync(existingBN);
            return StatusCode(204, res);
        }

        [HttpDelete("{benhNhanId}")]
        public async Task<IActionResult> DeletePatient(Guid benhNhanId)
        {
            var bn = await _patientService.GetByIdAsync(benhNhanId);
            var res = await _patientService.DeleteAsync(benhNhanId);
            
            var user = await _authService.FindByIdAsync(bn.UserId);
            if (user != null)
            {
                await _authService.DeleteUser(bn.UserId);
            }
            return StatusCode(201, res);
        }
    }
}
