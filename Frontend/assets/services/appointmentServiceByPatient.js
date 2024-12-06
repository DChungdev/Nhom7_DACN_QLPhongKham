var bnId;
var userId = localStorage.getItem("userId");
$(document).ready(function () {
  console.log(userId);

  //Lấy danh sách phòng ban, bác sĩ điền vào select trong form đăng ký
  const departmentSelect = $("#appointment #department");
  const doctorSelect = $("#appointment #doctor");
  const appointmentTimeSelect = $("#appointment #appointment-time");

  getAllDoctor(doctorSelect, appointmentTimeSelect);

  getAllDepartment(departmentSelect, doctorSelect, appointmentTimeSelect);

  //Lấy danh sách phòng ban, bác sĩ điền vào select trong modal edit
  const departmentSelectEdit = $("#modalEditAppointment #department-edit");
  const doctorSelectEdit = $("#modalEditAppointment #doctor-edit");
  const appointmentTimeSelectEdit = $(
    "#modalEditAppointment #appointment-time-edit"
  );

  getAllDoctor(doctorSelectEdit, appointmentTimeSelectEdit);

  getAllDepartment(
    departmentSelectEdit,
    doctorSelectEdit,
    appointmentTimeSelectEdit
  );
  //Lấy bệnh nhân theo userId
  getPatientByUserId();

  //Xử lý khi nhấn Đặt lịch khám
  $("#btnDatLich").click(function () {
    //Gọi hàm xử lý khi Đặt lịch
    registerAppointment();
  });

  //Xử lý khi nhấn Sửa lịch khám

  //Xử lý khi nhấn Hủy lịch khám
});

//Hàm lấy ra bệnh nhân theo userId
async function getPatientByUserId() {
  try {
    const response = await axiosJWT.get(`/api/Patients/getbyuserid/${userId}`);
    const object = response.data;
    console.log(object);
    bnId = object.benhNhanId; // Trả về id
  } catch (error) {
    console.error("Lỗi không tìm được bệnh nhân: ", error);
  }
}

//Hàm xử lý đặt lịch khám
function registerAppointment() {
  const patient = {
    maBenhNhan: "",
    hoTen: $("#appointment #name").val(),
    ngaySinh: $("#appointment #dateOfBirth").val() || null,
    loaiGioiTinh: parseInt($("#appointment #gender").val()) || null,
    soDienThoai: $("#appointment #phone").val(),
    email: $("#appointment #email").val(),
    diaChi: $("#appointment #address").val(),
    tienSuBenhLy: $("#appointment #message").val(),
  };
  const appointment = {
    lichKhamId: "00c3e5ab-e2c2-4265-aea1-c97b08d80ba6",
    benhNhanId: bnId,
    bacSiId: $("#appointment #doctor").val(),
    ngayKham: $("#appointment #appointment-date").val(),
    gioKham: $("#appointment #appointment-time").val(),
    trangThaiLichKham: "",
    benhNhan: patient,
  };
  //Check data hợp lệ
  checkData(appointment);
}

//Check Data hợp lệ
function checkData(appointment) {
  const errors = [];
  let firstErrorSelector = null; // Để lưu selector của lỗi đầu tiên
  const today = new Date().toISOString().split("T")[0]; // Ngày hiện tại

  // Hàm thêm lỗi cho input
  const setError = (selector, message) => {
    $(selector).addClass("input-error").attr("title", message);
    errors.push(message);
    if (!firstErrorSelector) firstErrorSelector = selector; // Lưu selector lỗi đầu tiên
  };

  // Hàm xóa lỗi khỏi input
  const clearError = (selector) => {
    $(selector).removeClass("input-error").removeAttr("title");
  };

  // Kiểm tra họ tên
  if (!appointment.benhNhan.hoTen.trim()) {
    setError("#appointment #name", "Họ tên không được để trống.");
  } else if (/\d/.test(appointment.benhNhan.hoTen)) {
    setError("#appointment #name", "Họ tên không được chứa số.");
  } else {
    clearError("#appointment #name");
  }

  // Kiểm tra ngày sinh
  if (appointment.benhNhan.ngaySinh) {
    if (appointment.benhNhan.ngaySinh >= today) {
      setError(
        "#appointment #dateOfBirth",
        "Ngày sinh phải nhỏ hơn ngày hiện tại."
      );
    } else {
      clearError("#appointment #dateOfBirth");
    }
  } else {
    clearError("#appointment #dateOfBirth");
  }

  // Kiểm tra số điện thoại
  if (!appointment.benhNhan.soDienThoai.trim()) {
    setError("#appointment #phone", "Số điện thoại không được để trống.");
  } else if (/\D/.test(appointment.benhNhan.soDienThoai)) {
    setError("#appointment #phone", "Số điện thoại không được chứa chữ.");
  } else {
    clearError("#appointment #phone");
  }

  // Kiểm tra email
  if (!appointment.benhNhan.email.trim()) {
    setError("#appointment #email", "Email không được để trống.");
  } else if (appointment.benhNhan.email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(appointment.benhNhan.email)) {
      setError("#appointment #email", "Email không đúng định dạng.");
    } else {
      clearError("#appointment #email");
    }
  } else {
    clearError("#appointment #email");
  }

  // Kiểm tra ngày khám
  if (!appointment.ngayKham.trim()) {
    setError(
      "#appointment #appointment-date",
      "Ngày khám không được để trống."
    );
  } else if (appointment.ngayKham < today) {
    setError(
      "#appointment #appointment-date",
      "Ngày khám không được nhỏ hơn ngày hiện tại."
    );
  } else {
    clearError("#appointment #appointment-date");
  }
  // Kiểm tra bác sĩ
  if (!appointment.bacSiId) {
    setError("#appointment #doctor", "Bác sĩ không được để trống.");
  } else {
    clearError("#appointment #doctor");
  }

  // Kiểm tra ca khám
  if (!appointment.gioKham.trim()) {
    setError("#appointment #appointment-time", "Ca khám không được để trống.");
  } else {
    clearError("#appointment #appointment-time");
  }

  // Nếu có lỗi, hiển thị danh sách lỗi và focus vào lỗi đầu tiên
  if (errors.length > 0) {
    showErrorList(errors, firstErrorSelector);
    // $(".sent-message").hide();
  } else {
    // Nếu không có lỗi, hiển thị thông báo thành công
    $(".error-message").hide();
    // $(".sent-message").show();
    saveAppointment(appointment); // Hàm lưu dữ liệu
  }
}

//Lưu lịch khám
function saveAppointment(appointment) {
  console.log(appointment);

  // Hiển thị trạng thái đang xử lý
  $(".loading").show();
  // Gửi yêu cầu đăng ký tới API
  axiosJWT
    .post(`/api/v1/Appointments`, appointment)
    .then(function (response) {
      console.log("Đăng ký thành công:", response.data);
      // Hiển thị trạng thái thành công
      $(".sent-message").show();
      $(".loading").hide();
    })
    .catch(function (error) {
      console.error("Lỗi khi thêm lịch khám: ", error);
      $(".sent-message").hide();
      $(".loading").hide();
    });
}

//Hiển thị danh sách lỗi
function showErrorList(errors, firstErrorSelector) {
  const errorContent = $("#appointment .error-message");
  errorContent.empty(); // Xóa nội dung cũ

  // Thêm lỗi dạng danh sách <ul>
  const errorList = $("<ul></ul>");
  errors.forEach((error) => {
    errorList.append(`<li>${error}</li>`);
  });
  errorContent.append(errorList);

  // Hiển thị thông báo lỗi
  errorContent.show();

  // Focus vào ô có lỗi đầu tiên
  if (firstErrorSelector) {
    $(firstErrorSelector).focus();
  }
}

// Lấy toàn bộ Bác sĩ
function getAllDoctor(selectElement, appointmentTimeSelect) {
  axiosJWT
    .get(`/api/Doctors`)
    .then(function (response) {
      const dsBacSi = response.data;

      // Đổ dữ liệu vào select bác sĩ
      dsBacSi.forEach((item) => {
        const option = $("<option>").val(item.bacSiId).text(item.hoTen);
        selectElement.append(option);
      });

      // Thêm sự kiện change
      selectElement.on("change", () => {
        addEventSelect(selectElement, dsBacSi, appointmentTimeSelect);
      });

      // Gọi hàm xử lý ban đầu
      addEventSelect(selectElement, dsBacSi, appointmentTimeSelect);
    })
    .catch(function (error) {
      console.error("Lỗi không tìm được:", error);
    });
}

//Tạo sự kiện khi chọn bác sĩ
function addEventSelect(selectElement, dsBacSi, appointmentTimeSelect) {
  const selectedDoctorId = selectElement.val();
  appointmentTimeSelect.html('<option value="">Chọn ca khám</option>'); // Reset giờ khám

  if (selectedDoctorId) {
    const selectedDoctor = dsBacSi.find(
      (doc) => doc.bacSiId == selectedDoctorId
    );
    if (selectedDoctor && selectedDoctor.gioLamViec) {
      const gioKhamArray = selectedDoctor.gioLamViec
        .split(",")
        .map((time) => time.trim());
      gioKhamArray.forEach((time) => {
        const timeOption = $("<option>").val(time).text(time);
        appointmentTimeSelect.append(timeOption);
      });
      appointmentTimeSelect.removeAttr("disabled");
    } else {
      appointmentTimeSelect.attr("disabled", true); // Disable nếu không có giờ khám
    }
  } else {
    appointmentTimeSelect.attr("disabled", true); // Disable nếu chưa chọn bác sĩ
  }
}

// Lấy toàn bộ khoa
function getAllDepartment(selectElement, doctorSelect, appointmentTimeSelect) {
  axiosJWT
    .get(`/api/v1/Departments`)
    .then(function (response) {
      const dsKhoa = response.data;

      // Đổ dữ liệu vào select khoa
      dsKhoa.forEach((item) => {
        const option = $("<option>").val(item.khoaId).text(item.tenKhoa);
        selectElement.append(option);
      });
    })
    .catch(function (error) {
      console.error("Lỗi không tìm được:", error);
    });

  // Lắng nghe sự kiện khi chọn Khoa
  selectElement.on("change", function () {
    const selectedKhoaId = selectElement.val();

    // Xóa các bác sĩ cũ
    doctorSelect.html('<option value="">Chọn bác sĩ</option>');
    appointmentTimeSelect.html('<option value="">Chọn ca khám</option>');

    if (selectedKhoaId) {
      // Lấy danh sách Bác sĩ theo Khoa
      axiosJWT
        .get(`/api/Doctors/doctor/${selectedKhoaId}`)
        .then(function (response) {
          const dsBacSi = response.data;

          // Đổ dữ liệu vào select bác sĩ
          dsBacSi.forEach((item) => {
            const option = $("<option>").val(item.bacSiId).text(item.hoTen);
            doctorSelect.append(option);
          });

          // Lắng nghe sự kiện chọn bác sĩ
          doctorSelect.off("change").on("change", function () {
            addEventSelect(doctorSelect, dsBacSi, appointmentTimeSelect);
          });
        })
        .catch(function (error) {
          console.error("Lỗi không tìm được bác sĩ:", error);
        });
    } else {
      // Lấy toàn bộ bác sĩ nếu không chọn khoa
      getAllDoctor(doctorSelect, appointmentTimeSelect);
    }
  });
}
