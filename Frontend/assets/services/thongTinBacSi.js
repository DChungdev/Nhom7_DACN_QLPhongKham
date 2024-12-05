$(document).ready(function () {
  getData(); //lấy danh sách bác sĩ từ API.

});
function getData() {
    var bacSiId = localStorage.getItem("bacSiId");
    console.log("BacSiId từ localStorage:", bacSiId); // Lấy bacSiId từ localStorage
    if (!bacSiId) {
        console.error("Không tìm thấy bacSiId trong localStorage");
        return;
    }

    axiosJWT
        .get(`/api/Doctors/${bacSiId}`) // Truyền đúng ID vào API
        .then(function (response) {
            var doctor = response.data; // API trả về 1 bác sĩ
            displayDoctors(doctor); // Hiển thị thông tin bác sĩ
        })
        .catch(function (error) {
            console.error("Lỗi không tìm được:", error);
        });
}


// Hàm hiển thị danh sách bác sĩ lên giao diện
function displayDoctors(data) {
    const bacSiInforByID = $("#bacSiInforByID"); // Vùng chứa thông tin bác sĩ
    bacSiInforByID.empty(); // Làm sạch trước khi thêm mới

    if (!data) {
        bacSiInforByID.append(`<p>Không tìm thấy bác sĩ nào.</p>`);
        return;
    }

    // Tạo HTML chi tiết cho bác sĩ
    const doctorHTML = `
        <div class="col-lg-4" data-aos="fade-up" data-aos-delay="100">
          <div class="team-member d-flex align-items-start">
            <div class="pic">
              <img
                src="../assets/img/doctors/doctors-1.jpg"
                class="img-fluid"
                alt=""
              />
            </div>
            <div class="member-info">
              <h4 id="tenBacSi">${data.hoTen}</h4>
              <span id="tenBangCap">${data.bangCap}</span>
              <div class="social">
                <a href=""><i class="bi bi-twitter-x"></i></a>
                <a href=""><i class="bi bi-facebook"></i></a>
                <a href=""><i class="bi bi-instagram"></i></a>
                <a href=""> <i class="bi bi-linkedin"></i> </a>
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-8" data-aos="fade-up" data-aos-delay="100">
          <div class="team-member d-flex align-items-start">
            <div class="member-info">
              <h4 id="tenBacSi">${data.hoTen}</h4>
              <span id="tenBangCap">${data.bangCap}</span>
              <p id="chuyenKhoa">Chuyên khoa: ${data.hoTen}</p>
              <p id="bangCap">Bằng cấp: ${data.bangCap}</p>
              <p id="soNamCongTac">Số năm công tác: ${data.soNamKinhNghiem} năm</p>
              <p id="gioLamViec">Giờ làm việc: ${data.gioLamViec}</p>
            </div>
          </div>
        </div>
    `;

    bacSiInforByID.append(doctorHTML); // Thêm thông tin vào vùng chứa
}
