using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Business.Interfaces;
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

        public ResultsController(IResultService serviceService, IAuthService authService, IMapper mapper)
        {
            this._resultService = serviceService;
            this._authService = authService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> getKetQuaKham()
        {
            var ketquas = await _resultService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResultModel>>(ketquas));
        }

        [HttpGet("{KetQuaKhamId}")]
        public async Task<ActionResult> GetKetQuaKhamById(Guid ketQuaKhamId)
        {
            try
            {
                // Gọi service để lấy thông tin KetQuaKham
                var ketQuaKham = await _resultService.GetByIdAsync(ketQuaKhamId);

                // Kiểm tra nếu không tìm thấy
                if (ketQuaKham == null)
                {
                    return NotFound($"Không tìm thấy kết quả khám với ID: {ketQuaKhamId}");
                }

                // Map từ entity sang model để trả về cho client
                var ketQuaKhamModel = _mapper.Map<ResultModel>(ketQuaKham);
                return Ok(ketQuaKhamModel);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi 500
                return StatusCode(500, $"Lỗi khi lấy thông tin kết quả khám: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddKetQuaKham([FromBody] ResultModel ketQuaModel)
        {
            // Kiểm tra nếu model không hợp lệ
            if (ketQuaModel == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            // Chuyển đổi từ DichVuModel sang DichVu entity
            var ketQuaEntity = _mapper.Map<KetQuaKham>(ketQuaModel);

            try
            {
                // Thêm dịch vụ mới
                int result = await _resultService.AddAsync(ketQuaEntity);

                // Trả về kết quả thành công
                return CreatedAtAction(nameof(getKetQuaKham), new { id = ketQuaEntity.KetQuaKhamId }, ketQuaEntity);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong quá trình thêm
                return StatusCode(500, $"Lỗi khi thêm kết quả khám: {ex.Message}");
            }
        }

        //[HttpPut("{DichVuId}")]
        //public async Task<ActionResult> UpdateDichVu(Guid DichVuId, [FromBody] DichVuModel dichVuModel)
        //{
        //	if (DichVuId != dichVuModel.DichVuId)
        //	{
        //		return BadRequest("Id không giống!");
        //	}

        //	var existingDV = await _serviceService.GetByIdAsync(DichVuId);

        //	if (existingDV == null)
        //	{
        //		return NotFound("Không tìm thấy dịch vụ để sửa");
        //	}
        //	var properties = dichVuModel.GetType().GetProperties();
        //	foreach (var property in properties)
        //	{
        //		var value = property.GetValue(dichVuModel) as string;
        //		if (string.IsNullOrEmpty(value))
        //		{
        //			return BadRequest($"Trường {property.Name} không được để trống.");
        //		}
        //	}
        //	// Cập nhật các trường thông tin dịch vụ
        //	existingDV.MaDichVu = dichVuModel.MaDichVu;
        //	existingDV.TenDichVu = dichVuModel.TenDichVu;
        //	existingDV.MoTaDichVu = dichVuModel.MoTaDichVu;
        //	existingDV.DonGia = dichVuModel.DonGia;
        //	existingDV.NgayCapNhat = DateTime.Now;
        //	existingDV.KhoaId = dichVuModel.KhoaId;

        //	// Cập nhật dịch vụ
        //	int res = await _serviceService.UpdateAsync(existingDV);
        //	if (res > 0)
        //	{
        //		return NoContent();  // Thành công
        //	}
        //	else
        //	{
        //		return StatusCode(500, "Cập nhật dịch vụ không thành công.");  // Thất bại
        //	}
        //}

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
    }
}
