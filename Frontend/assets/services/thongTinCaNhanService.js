// Lưu danh sách dịch vụ toàn bộ
var services = []; 

$(document).ready(function () {
    // Gọi API để tải danh sách dịch vụ
    loadServices();

    // Thêm sự kiện tìm kiếm khi nhập vào ô tìm kiếm
    $("#searchService").on("input", function () {
        const searchValue = $(this).val().toLowerCase(); // Lấy giá trị tìm kiếm
        filterServices(searchValue); // Gọi hàm lọc dịch vụ
    });
});

// Hàm gọi API để lấy danh sách dịch vụ
function loadServices() {
    axiosJWT
        .get(`/api/Services`) // Gọi API lấy danh sách dịch vụ
        .then(function (response) {
            services = response.data; // Lưu dữ liệu từ API vào mảng services
            displayServices(services); // Hiển thị danh sách dịch vụ lên giao diện
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách dịch vụ:", error); // Xử lý lỗi nếu có
            showErrorPopup("Không thể tải danh sách dịch vụ. Vui lòng thử lại.");
        });
}

// Hàm hiển thị danh sách dịch vụ lên giao diện
function displayServices(data) {
    const serviceList = $("#serviceList"); // Lấy phần tử HTML chứa danh sách dịch vụ
    serviceList.empty(); // Xóa các dịch vụ cũ trước khi hiển thị mới

    if (data.length === 0) {
        serviceList.append(`<p>Không tìm thấy dịch vụ nào.</p>`); // Hiển thị thông báo nếu không có dịch vụ
        return;
    }

    // Lặp qua danh sách dịch vụ và tạo HTML cho từng dịch vụ
    data.forEach((service) => {
        const serviceHTML = `
            <div class="col-md-4 mb-3 service-item">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${service.tenDichVu}</h5>
                        <p class="card-text">Giá: ${service.donGia.toLocaleString()}đ</p>
                        <p class="card-text">${service.moTaDichVu || "Không có mô tả"}</p>
                    </div>
                </div>
            </div>
        `;
        serviceList.append(serviceHTML); // Thêm HTML vào vùng chứa dịch vụ
    });
}

// Hàm lọc danh sách dịch vụ theo giá trị tìm kiếm
function filterServices(searchValue) {
    const filteredServices = services.filter((service) =>
        service.tenDichVu.toLowerCase().includes(searchValue) // Kiểm tra tên dịch vụ có chứa từ khóa không
    );
    displayServices(filteredServices); // Hiển thị danh sách dịch vụ sau khi lọc
}

// Hàm hiển thị thông báo lỗi
function showErrorPopup(message) {
    const errorPopup = document.createElement("div");
    errorPopup.className = "alert alert-danger";
    errorPopup.style.position = "fixed";
    errorPopup.style.bottom = "20px";
    errorPopup.style.right = "20px";
    errorPopup.style.zIndex = "9999";
    errorPopup.innerText = message;

    document.body.appendChild(errorPopup);

    // Ẩn thông báo sau 3 giây
    setTimeout(() => {
        document.body.removeChild(errorPopup);
    }, 3000);
}
