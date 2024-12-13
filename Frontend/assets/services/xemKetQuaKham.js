let results = []; // Biến lưu trữ toàn bộ danh sách kết quả

$(document).ready(function () {
    loadResults(); // Tải danh sách kết quả khi trang được load
});

function loadResults() {
    axiosJWT.get('/api/Results')
        .then((response) => {
            results = response.data;
            displayResults(results); // Hiển thị danh sách kết quả
        })
        .catch((error) => {
            console.error('Lỗi khi tải danh sách kết quả:', error);
        });
}

// Hàm hiển thị danh sách kết quả
function displayResults(results) {
    const resultTableBody = $('#resultList'); // Xác định phần tbody của bảng
    resultTableBody.empty(); // Xóa nội dung cũ trước khi thêm mới

    if (results.length === 0) {
        resultTableBody.append('<tr><td colspan="9">Không có kết quả khám nào.</td></tr>'); // Hiển thị thông báo nếu không có dữ liệu
        return;
    }

    // Lặp qua danh sách kết quả và tạo từng dòng
    results.forEach((result, index) => {
        const resultRow = `
            <tr>
                <td>${result.ketQuaKhamId}</td>                                 
                <td>${formatDate(result.ngayTao)}</td>           
                <td>${result.chanDoan || "Không có chẩn đoán"}</td>
                <td>${result.chiDinhThuoc || "Không có chỉ định thuốc"}</td>
                <td class="text-center">
                      <a href="Frontend/User/DanhGiaPhanhoi.html" class="btn btn-outline-primary btn-sm fw-bold rounded-pill">Đánh giá</a>
                  </td>
            </tr>
        `;
        resultTableBody.append(resultRow); // Thêm dòng vào bảng
        console.log("abc",result)
    });
}
// Hàm định dạng ngày (nếu ngày không null)
function formatDate(dateString) {
    if (!dateString) return "Không có dữ liệu";
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN'); // Định dạng theo ngày Việt Nam
}

function showErrorPopup() {
    const errorPopup = document.getElementById("error-popup");
    errorPopup.style.visibility = "visible";

    // Ẩn popup sau 3 giây
    setTimeout(() => {
        hideErrorPopup();
    }, 3000);
}
function hideErrorPopup() {
    const errorPopup = document.getElementById("error-popup");
    errorPopup.style.visibility = "hidden";
}