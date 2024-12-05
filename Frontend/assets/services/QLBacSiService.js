var dsBS;
var bsID = "";

$(document).ready(function () {
    getData();
    loadDepartments();

    // Gắn sự kiện cho nút hiển thị modal sửa
    $(document).on("click", ".m-edit", function () {
        // const benhNhanId = $(this).data("id"); // Lấy id từ thuộc tính data-id
        const bacSiId = $(this).closest("tr").attr("bs-id");
        bsID = bacSiId;
        const bacSi = dsBS.find((bs) => bs.bacSiId === bacSiId); // Tìm bệnh nhân trong danh sách
   
        if (bacSi) {
            fillEditModal(bacSi); // Hiển thị thông tin lên modal
        } else {
            console.error("Không tìm thấy thông tin bác sĩ!");
        }
    });

    // Gắn sự kiện cho nút hiển thị modal Thêm
    $("#btnOpenModalAdd").click(function(){
        let maBSNext = getMaxBacSiCode(dsBS);
        $("#add-mabs").val(maBSNext);
    })
    // Gắn sự kiện cho nút Thêm
    $("#btnAdd").click(function (){
        const bangCapValue = parseInt($("#add-bangCap").val());
        const khoaIdValue = $("#add-khoa").val(); // Lấy ID khoa từ combobox
        const newDoctor = {
            maBacSi: $("#add-mabs").val(),
            hoTen: $("#add-tenbs").val(),
            email: $("#add-email").val() || null,
            soDienThoai: $("#add-sdt").val() || null,
            bangCap: bangCapValue,
            diaChi: $("#add-diaChi").val() || null,
            soNamKinhNghiem: $("#add-namKinhNghiem").val() || null,
            gioLamViec: $("#add-gioLamViec").val() || null,
            khoaId: khoaIdValue || null,
        };
        console.log("Dữ liệu thêm:", newDoctor);

        // Gửi yêu cầu add tới API
        axiosJWT
            .post(`/api/Doctors`, newDoctor)
            .then(function (response) {
                console.log("Thêm thành công:", response.data);
                getData(); // Tải lại dữ liệu sau khi cập nhật
            })
            .catch(function (error) {
                showErrorPopup();
                console.error("Lỗi khi thêm:", error);
            });
    });

    // Gắn sự kiện cho nút Sửa
    $("#btnEdit").click(function () {
        const bangCapValue = parseInt($("#edit-bangCap").val());
        const updatedDoctor = {
            bacSiId: bsID,
            maBacSi: $("#edit-mabs").val(),
            hoTen: $("#edit-tenbs").val(),
            email: $("#edit-email").val(),
            soDienThoai: $("#edit-sdt").val() || null,
            bangCap: bangCapValue,
            diaChi: $("#edit-diaChi").val() || null,
            soNamKinhNghiem: $("#edit-namKinhNghiem").val() || null,
            gioLamViec: $("#edit-gioLamViec").val() || null,
        };

        console.log("Dữ liệu cập nhật:", updatedDoctor);

        // Gửi yêu cầu cập nhật tới API
        axiosJWT
            .put(`/api/Doctors/${updatedDoctor.bacSiId}`, updatedDoctor)
            .then(function (response) {
                console.log("Cập nhật thành công:", response.data);
                getData(); // Tải lại dữ liệu sau khi cập nhật
                showSuccessPopup();
            })
            .catch(function (error) {
                showErrorPopup();
                console.error("Lỗi khi cập nhật:", error);
            });
    });

    //Mở modal xác nhận xóa
    $(document).on("click", ".m-delete", function () {
        const bacSiId = $(this).closest("tr").attr("bs-id");
        bsID = bacSiId;
        console.log(bsID);
        const bacSi = dsBS.find((bs) => bs.bacSiId === bacSiId); // Tìm bệnh nhân trong danh sách
    });

    $("#btnDelete").click(function(){
        axiosJWT
            .delete(`/api/Doctors/${bsID}`)
            .then(function (response) {
                console.log("Xóa thành công:", response.data);
                getData(); // Tải lại dữ liệu sau khi cập nhật
            })
            .catch(function (error) {
                showErrorPopup();
                console.error("Lỗi khi xóa:", error);
            });
    });
});

function getData() {
    // var userId = localStorage.getItem("userId");
    // $('#hotenHeader').text(localStorage.getItem(loggedInUsername));
    axiosJWT
        .get(`/api/Doctors`)
        .then(function (response) {
            dsBS = response.data;
            console.log(dsBS);
            display(dsBS);
        })
        .catch(function (error) {
            console.error("Lỗi không tìm được:", error);
        });
}
function display(data) {
    const tableBody = document.querySelector('#tblBacSi tbody');
    tableBody.innerHTML = ''; // Xóa nội dung cũ nếu có

    data.forEach((item, index) => {
        const row = `
      <tr bs-id="${item.bacSiId}">
        <td class="text-center">${index + 1}</td>
        <td class="m-data-left">${item.maBacSi}</td>
        <td class="m-data-left">${item.hoTen}</td>
        <td class="m-data-left">${item.tenBangCap}</td>
        <td class="m-data-left">${item.soDienThoai}</td>
        <td class="m-data-left">${item.email || "Chưa có email"}</td>
        <td class="m-data-left">${item.diaChi || "Chưa có địa chỉ"}</td>
        <td>
                  <div class="m-table-tool">
                    <div class="m-edit m-tool-icon" data-bs-toggle="modal" data-bs-target="#modalSuaBacSi" data-id="${item.bacSiId}">
                      <i class="fas fa-edit text-primary"></i>
                    </div>
                    <div class="m-delete m-tool-icon" data-bs-toggle="modal" data-bs-target="#confirm-delete">
                      <i class="fas fa-trash-alt text-danger"></i>
                    </div>
                  </div>
                </td>
      </tr>
    `;
        tableBody.innerHTML += row; // Thêm hàng vào bảng
    });
}
// Gọi API lấy danh sách khoa
function loadDepartments() {
    axiosJWT
        .get(`/api/Departments`)
        .then(function (response) {
            const departments = response.data;
            const departmentDropdown = $("#add-khoa");
            
            // Xóa các option cũ (nếu có)
            departmentDropdown.empty();

            // Thêm option mặc định
            departmentDropdown.append('<option value="">-- Chọn khoa --</option>');

            // Đổ dữ liệu vào combobox
            departments.forEach(function (khoa) {
                departmentDropdown.append(
                    `<option value="${khoa.khoaId}">${khoa.tenKhoa}</option>`
                );
            });
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách khoa:", error);
        });
}

// Hàm điền thông tin vào modal
function fillEditModal(bacSi) {
    // Gán dữ liệu vào các trường input của modal
    $("#edit-mabs").val(bacSi.maBacSi); // Mã bệnh nhân
    $("#edit-tenbs").val(bacSi.hoTen); // Họ tên
    $("#edit-sdt").val(bacSi.soDienThoai); // Số điện thoại
    $("#edit-email").val(bacSi.email || ""); // Email
    // Gán bằng cấp
    const bangCapValue = bacSi.bangCap;
    $("#edit-bangCap").val(bangCapValue);
    $("#edit-namKinhNghiem").val(bacSi.soNamKinhNghiem); // Số năm kinh nghiệm
    // Gán địa chỉ
    $("#edit-diaChi").val(bacSi.diaChi || "");
    $("#edit-gioLamViec").val(bacSi.gioLamViec); // Giờ làm việc

}

function formatDate(dateString) {
    if (!dateString) return "Không xác định";
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' });
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

// Hàm lấy mã bác sĩ lớn nhất
function getMaxBacSiCode(dsBS) {
    let maxCode = 0;
    dsBS.forEach(item => {
        const code = parseInt(item.maBacSi.replace('BS', '')); // Loại bỏ 'BS' và chuyển thành số
        if (code > maxCode) {
            maxCode = code;
        }
    });
    const nextCode = maxCode + 1;
    return 'BS' + nextCode.toString().padStart(3, '0');
}
function showSuccessPopup() {
    // Hiển thị popup
    const popup = document.getElementById("success-popup");
    popup.style.visibility = "visible";  // Hoặc có thể dùng popup.classList.add('visible');

    // Tự động ẩn popup sau 3 giây (3000ms)
    setTimeout(() => {
        closePopup();
    }, 3000);
}

function closePopup() {
    const popup = document.getElementById("success-popup");
    popup.style.visibility = "hidden";  // Ẩn popup
}
function kiemTraTen() {
    const ten = document.getElementById("add-tenbs");
    const specialCharRegex = /[^a-zA-Z0-9\s\u00C0-\u1EF9]/; // Tìm bất kỳ ký tự nào không phải là chữ cái, chữ số hoặc khoảng trắng

    if (specialCharRegex.test(ten.value)) {
      ten.style.border = "2px solid red"; // Thêm viền đỏ khi có ký tự đặc biệt
      ten.setCustomValidity("Tên không được chứa ký tự đặc biệt!");
    } else {
      ten.style.border = ""; // Xóa viền nếu không có ký tự đặc biệt
      ten.setCustomValidity("");
    }
  }
  function kiemTraEmail() {
    const emailInput = document.getElementById("add-email");
    // Biểu thức chính quy kiểm tra định dạng email hợp lệ
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

    // Kiểm tra nếu email trống
    if (emailInput.value.trim() === "") {
        emailInput.style.border = "2px solid red"; // Viền đỏ khi trống
        emailInput.setCustomValidity("Email không được để trống");
    } 
    // Kiểm tra email hợp lệ
    else if (!emailRegex.test(emailInput.value)) {
        emailInput.style.border = "2px solid red"; // Viền đỏ nếu email không hợp lệ
        emailInput.setCustomValidity("Email không hợp lệ");
    } else {
        emailInput.style.border = ""; // Xóa viền đỏ nếu email hợp lệ
        emailInput.setCustomValidity(""); // Không có lỗi
    }
}