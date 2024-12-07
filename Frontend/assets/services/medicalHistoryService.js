var userId = localStorage.getItem("userId");
var lkId = "";
$(document).ready(function () {
  console.log(userId);

  // Lắng nghe sự kiện click trên các nút .optionButton
  $(document).on("click", ".optionButton", function () {
    // Tìm phần tử cha gần nhất có lớp .custom-card
    const parentCard = $(this).closest(".custom-card");

    // Lấy giá trị lkId từ thuộc tính của phần tử cha
    lkId = parentCard.attr("lkId");
  });
  //Xử lý khi nhấn nút đồng ý hủy
  $("#btnCancel").click(function () {
    console.log(lkId);
    //Gọi API Hủy lịch khám
    cancelAppointment();
  });
  //Xử lý khi nhấn nút đồng ý xóa
  $("#btnDelete").click(function () {
    console.log(lkId);
    //Gọi API Hủy lịch khám
    deleteAppointment();
  });
});

//Xử lý khi nhấn đồng ý xóa lịch khám
function deleteAppointment() {
  // Hiển thị trạng thái đang xử lý
  $("#modal-confirm-cancel #btnDelete")
    .prop("disabled", true)
    .text("Đang xóa...");
  axiosJWT
    .delete(`/api/v1/Appointments/${lkId}`)
    .then(function (response) {
      console.log("Xóa lịch khám thành công:", response.data);
      getAvata();
      showPopup("success", "Thành công! Lịch khám đã được xóa.");
      $("#modal-confirm-cancel #btnDelete").prop("disabled", false).text("Xóa");
    })
    .catch(function (error) {
      showPopup("error", "Lỗi! Không thể xóa lịch khám.");
      $("#modal-confirm-cancel #btnDelete").prop("disabled", false).text("Xóa");
      getAvata();
      console.error("Lỗi khi xóa lịch khám: ", error);
    });
}
//Xử lý khi nhấn đồng ý hủy lịch khám
function cancelAppointment() {
  // Hiển thị trạng thái đang xử lý
  $("#modal-confirm-cancel #btnCancel")
    .prop("disabled", true)
    .text("Đang xử lý...");
  axiosJWT
    .put(`/api/v1/Appointments/cancel/${lkId}`)
    .then(function (response) {
      console.log("Hủy lịch khám thành công:", response.data);
      getAvata();
      showPopup("success", "Thành công! Lịch khám đã được hủy.");
      $("#modal-confirm-cancel #btnCancel").prop("disabled", false).text("Có");
    })
    .catch(function (error) {
      getAvata();
      showPopup("error", "Lỗi! Không thể hủy lịch khám.");
      $("#modal-confirm-cancel #btnCancel").prop("disabled", false).text("Có");
      console.error("Lỗi khi hủy lịch khám: ", error);
    });
}
//Hàm hiển thị popup thông báo
function showPopup(type, message) {
  const popupItem = $(`.m-popup-item.m-popup-${type}`);

  // Xóa nội dung trước đó
  popupItem.find(".m-popup-text").empty();

  // Cập nhật nội dung thông báo
  const [title, detail] = message.split("! ");
  popupItem
    .find(".m-popup-text")
    .append(`<span>${title}! </span>`)
    .append(detail);

  // Hiển thị popup block
  popupItem.addClass("show");

  // Ẩn popup sau 3 giây
  setTimeout(() => {
    popupItem.removeClass("show");
  }, 3000);

  // Đảm bảo tắt popup nếu người dùng đóng
  popupItem.find(".m-popup-close").on("click", function () {
    // Đóng popup
    popupItem.removeClass("show");
  });
}
//Lấy avartar
function getAvata() {
  axiosJWT
    .get(`/api/Patients/getbyuserid/${userId}`)
    .then(function (response) {
      const bn = response.data;
      console.log(bn);
      if (bn.hinhAnh != null) {
        $("#avatar").attr("src", "http://localhost:37649" + bn.hinhAnh);
      }
      //Lấy ra danh sách lịch khám theo benhNhanId
      getAllAppointmentByBenhNhanId(bn.benhNhanId);
    })
    .catch(function (error) {
      console.error("Lỗi không tìm được:", error);
    });
}
//Lấy danh sách lịch khám
function getAllAppointmentByBenhNhanId(benhNhanId) {
  console.log(benhNhanId);
  axiosJWT
    .get(`/api/v1/Appointments/patient/${benhNhanId}`)
    .then(function (response) {
      const dsLK = response.data;
      display1(dsLK);
    })
    .catch(function (error) {
      console.error("Lỗi không tìm được:", error);
    });
}
// Hàm để chuyển trạng thái thành lớp CSS
function getBadgeClass(trangThai) {
  switch (trangThai) {
    case "Đã đặt":
      return "badge-booked";
    case "Đang xử lý":
      return "bg-primary";
    case "Đã hủy":
      return "bg-danger";
    default:
      return "bg-success";
  }
}

//Lấy tên bác sĩ theo id
async function getNameById(id) {
  try {
    const response = await axiosJWT.get(`/api/Doctors/${id}`);
    const object = response.data;
    return object.hoTen; // Trả về họ tên
  } catch (error) {
    console.error("Lỗi không tìm được bác sĩ: ", error);
    return null; // Trả về null nếu có lỗi
  }
}

//Hiển thị lịch khám theo benhNhanId
async function display1(dsLK) {
  console.log(dsLK);
  // Hiển thị dữ liệu
  const container = $("#container_lk .row");
  container.empty(); // Xóa nội dung cũ

  // Tạo danh sách các Promise để lấy tên bác sĩ
  const doctorNamesPromises = dsLK.map((item) => getNameById(item.bacSiId));
  const doctorNames = await Promise.all(doctorNamesPromises); // Chờ tất cả Promise hoàn thành

  dsLK.forEach((lichKham, index) => {
    // Lấy tên bệnh nhân từ mảng đã xử lý
    const doctorName = doctorNames[index];
    // // Định dạng hiển thị dd/MM/yyyy
    const dateString = lichKham.ngayKham;
    const date = new Date(dateString);
    const formattedDate = date.toLocaleDateString("en-GB"); // 'en-GB' chuẩn Anh (ngày/tháng/năm)
    // Xử lý trạng thái lịch khám
    const editDisabled =
      lichKham.trangThaiLichKham === "Đã hủy" ||
      lichKham.trangThaiLichKham === "Hoàn thành"
        ? "disabled"
        : "";
    const col = `
          <div class="col-md-4">
              <div class="card custom-card" lkId="${lichKham.lichKhamId}">
                  <div class="d-flex justify-content-between align-items-start">
                      <span class="badge rounded-pill ${getBadgeClass(
                        lichKham.trangThaiLichKham
                      )}" style="min-width: 70px">
                          ${lichKham.trangThaiLichKham}
                      </span>
                      <span class="dropdown">
                          <button class="optionButton btn btn-link dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                              <i class="fas fa-ellipsis-v"></i>
                          </button>
                          <ul class="dropdown-menu">
                              <li>
                                  <div class="dropdown-item ${editDisabled}" data-bs-target="#modal-confirm-cancel" data-bs-toggle="modal">
                                      <i class="fas fa-times-circle me-2" style="color: rgb(255, 123, 0)"></i> Hủy
                                  </div>
                              </li>
                              <li>
                                  <div class=" dropdown-item" data-bs-target="#modal-confirm-delete" data-bs-toggle="modal">
                                      <i class="fas fa-trash-alt me-2" style="color: rgb(245, 76, 76)"></i> Xóa
                                  </div>
                              </li>
                          </ul>
                      </span>
                  </div>
                  <div class="mt-2">
                      <h5 class="mb-1">Bác sĩ: ${doctorName}</h5>
                      <p class="mb-1">Ngày khám: ${formattedDate}</p>
                      <p class="mb-0">Ca khám: ${lichKham.gioKham}</p>
                  </div>
              </div>
          </div>
      `;

    container.append(col);
  });
}
