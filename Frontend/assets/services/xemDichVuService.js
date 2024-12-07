var services = []; // Lưu danh sách dịch vụ toàn bộ
let khoas = []; 

$(document).ready(function () {
    loadKhoas();
    loadServices();
});

// Hàm lấy danh sách dịch vụ từ API
function loadServices() {
    axiosJWT
        .get(`/api/Services`)
        .then(function (response) {
            services = response.data; // Lưu dữ liệu từ API
            displayServices(services); // Hiển thị toàn bộ dịch vụ
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách dịch vụ:", error);
        });
}

function loadKhoas() {
    axiosJWT
        .get(`/api/v1/Departments`)
        .then(function (response) {
            khoas = response.data;
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách khoa:", error);
        });
}

// Hàm hiển thị danh sách dịch vụ
function displayServices(data) {
    const serviceList = $("#serviceList"); // Vùng chứa danh sách dịch vụ
    serviceList.empty(); // Xóa các dịch vụ cũ

    if (data.length === 0) {
        serviceList.append(`<p>Không tìm thấy dịch vụ nào.</p>`);
        return;
    }

    // Lặp qua danh sách dịch vụ và tạo HTML cho từng dịch vụ
    data.forEach((service) => {
        const khoa = khoas.find(k => k.khoaId === service.khoaId);
        const khoaName = khoa ? khoa.tenKhoa : "Tất cả";
        const serviceHTML = `
            <div class="col-md-4 mb-3 service-item">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title" id="tenDichVu">${service.tenDichVu}</h5>
                        <p class="card-text" id="giaDichVu">Giá: ${service.donGia.toLocaleString()}đ</p>
                        <p class="card-text" id="tenKhoa">Khoa: ${khoaName}</p>
                        <p class="card-text" id="moTaDichVu">Mô tả: ${service.moTaDichVu || "Không có mô tả"}</p>
                    </div>
                </div>
            </div>
        `;
        serviceList.append(serviceHTML); // Thêm dịch vụ vào vùng chứa
    });
}


// Hàm lọc danh sách dịch vụ dựa trên giá trị tìm kiếm
function filterServices(searchValue) {
    const filteredServices = services.filter((service) =>
        service.tenDichVu.toLowerCase().includes(searchValue)
    );
    displayServices(filteredServices); // Hiển thị các dịch vụ tìm được
}
