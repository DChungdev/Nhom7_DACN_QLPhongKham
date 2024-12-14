using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Business.Services;
using QuanLyPhongKham.Models.Entities;
using QuanLyPhongKham.Models.Models;

namespace QuanLyPhongKham.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public ResultsController(IResultService resultService, IAuthService authService, IMapper mapper, IPatientService patientService, IAppointmentService appointmentService)
        {
            _resultService = resultService;
            _authService = authService;
            _mapper = mapper;
            _patientService = patientService;
            _appointmentService = appointmentService;


        }

        [HttpGet]
        public async Task<ActionResult> getKetQuaKham()
        {
            var ketquas = await _resultService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResultModel>>(ketquas));
        }

        [HttpGet("{KetQuaKhamId}")]
        public async Task<IActionResult> GetResultById(Guid KetQuaKhamId)
        {
            var result = await _resultService.GetByIdAsync(KetQuaKhamId);
            return Ok(_mapper.Map<ResultModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KetQuaKham ketQuaKham)
        {

            if (ketQuaKham.LichKhamId == Guid.Empty) // Kiểm tra LichKhamId có hợp lệ không
            {
                return BadRequest("LichKhamId không hợp lệ.");
            }
            int result = await _resultService.AddAsync(ketQuaKham);
            if (result > 0)
            {
                return StatusCode(201, "Thêm mới kết quả khám thành công.");
            }
            else
            {
                return BadRequest("Thêm mới kết quả khám thất bại.");
            }

        }




        [HttpPut("{ketQuaKhamId}")]
        public async Task<IActionResult> UpdateService(Guid ketQuaKhamId, [FromBody] ResultModel ketQuaKham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ketQuaKhamId != ketQuaKham.KetQuaKhamId)
            {
                return BadRequest("Id không giống!");
            }

            var existingKQ = await _resultService.GetByIdAsync(ketQuaKhamId);
            if (existingKQ == null)
            {
                Console.WriteLine($"Không tìm thấy kết quả khám với ID: {ketQuaKhamId}");
                return BadRequest("Không tìm thấy kết quả khám");
            }


            existingKQ.ChanDoan = ketQuaKham.ChanDoan;
            existingKQ.ChiDinhThuoc = ketQuaKham.ChiDinhThuoc;
            existingKQ.GhiChu = ketQuaKham.GhiChu;
            existingKQ.NgayCapNhat = DateTime.Now;

            int res = await _resultService.UpdateAsync(existingKQ);
            Console.WriteLine($"UpdateAsync result: {res}");
            return StatusCode(204, res);
        }

        [HttpGet("ketqua/{LichKhamId}")]
        public ActionResult GetKetQuaByLichKhamId(Guid LichKhamId)
        {
            var kq = _resultService.GetKetQuaKhamByLichKhamId(LichKhamId);
            return Ok(kq);
        }
    }
}
