var doctors = []; //Khai báo mảng lưu trữ danh sách các bác sĩ được lấy từ API.
$(document).ready(function () {
    loadDoctors(); //lấy danh sách bác sĩ từ API.
  
});
function loadDoctors() {
    axiosJWT
        .get(`/api/Doctors`) //Gửi một yêu cầu GET đến API /api/Doctors để lấy danh sách bác sĩ.
        .then(function (response) {
            doctors = response.data; // Lưu dữ liệu từ API
            displayDoctors(doctors); // Hiển thị toàn bộ dịch vụ
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách bác sĩ", error);
        });
}
// Hàm hiển thị danh sách bác sĩ lên giao diện
function displayDoctors(data) {
    const doctorsList = $("#doctorsList"); // Vùng chứa danh sách bác sĩ
    doctorsList.empty(); // làm sạch danh sách trước khi thêm mới.

    if (data.length === 0) {
        doctorsList.append(`<p>Không tìm thấy bác sĩ nào.</p>`);
        return;
    }

    // Lặp qua danh sách bác sĩ và tạo HTML cho từng bác sĩ
    data.forEach((doctor) => {
        const doctorHTML = `
            <div class="col-lg-6" data-aos="fade-up" data-aos-delay="100">
                <a href="ThongTinBacSi.html" style="color: inherit; text-decoration: none;" onclick="storeDoctorId(${doctor.bacSiId})">
                    <div class="team-member d-flex align-items-start">
                    <div class="pic"><img src="../assets/img/doctors/doctors-1.jpg" class="img-fluid" alt=""></div>
                    <div class="member-info">
                        <h4 id="tenBacSi">${doctor.hoTen}</h4>
                        <span id="tenBangCap">${doctor.tenBangCap}</span>
                        <div class="social">
                            <i class="bi bi-twitter-x"></i>
                            <a href=""><i class="bi bi-facebook"></i></a>
                            <a href=""><i class="bi bi-instagram"></i></a>
                            <a href=""> <i class="bi bi-linkedin"></i> </a>
                        </div>
                    </div>
                    </div>
                </a>
            </div>
        `;
        doctorsList.append(doctorHTML); // Thêm bác sĩ vào vùng chứa
    });
}
// Lưu ID bác sĩ vào localStorage khi nhấp vào
function storeDoctorId(bacSiId) {
    localStorage.setItem("bacSiId", bacSiId);
}
