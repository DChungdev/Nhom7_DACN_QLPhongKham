var dsBN;
var bnID = "";
$(document).ready(function () {
    getData();
});

function getData() {
    axiosJWT
        .get(`/api/Patients/getbydoctorid/${bacSiId}`)
        .then(function (response) {
            dsBN = response.data;
            console.log(dsBN);
            display(dsBN);
        })
        .catch(function (error) {
            console.error("Lỗi không tìm được:", error);
        });
}

function display(data) {
    const tableBody = document.querySelector('#tblBenhNhan tbody');
    tableBody.innerHTML = ''; // Xóa nội dung cũ nếu có

    data.forEach((item, index) => {
        const row = `
      <tr bn-id="${item.benhNhanId}">
        <td class="chk">
          <input type="checkbox" />
        </td>
        <td class="m-data-left">${index + 1}</td>
        <td class="m-data-left">${item.maBenhNhan}</td>
        <td class="m-data-left">${item.hoTen}</td>
        <td class="m-data-left">${item.gioiTinh || "Không xác định"}</td>
        <td class="m-data-left">${formatDate(item.ngaySinh)}</td>
        <td class="m-data-left">${item.email || "Chưa có email"}</td>
        <td class="m-data-left">${item.diaChi || "Chưa có địa chỉ"}</td>
    `;
        tableBody.innerHTML += row; // Thêm hàng vào bảng
    });
}

function formatDate(dateString) {
    if (!dateString) return "Không xác định";
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' });
}

