let services = []; // Biến lưu trữ toàn bộ danh sách dịch vụ

$(document).ready(function () {
    loadServices(); // Tải danh sách dịch vụ khi trang được load
    loadKhoas(); // Gọi hàm để tải danh sách khoa
    $(".m-input-search").on("input", function () {
        const searchValue = removeAccents($(this).val().toLowerCase()); // Lấy giá trị nhập vào và chuẩn hóa
        filterServices(searchValue); // Gọi hàm lọc danh sách
    });
    
    // Gắn sự kiện cho nút hiển thị modal Thêm
    $("#btnThemMoi").click(function(){
        let maDVNext = getMaxDichVuCode(services);
        $('#dialog-add input[type="text"]').val(""); 
        $('#khoaSelect').val("");
        $('#dialog-add input[type="text"]').eq(1).val(maDVNext);
        loadKhoas('khoaSelect');

    })

    // Sự kiện thêm mới dịch vụ
    $('#btnAdd').on('click', function () {
        const khoaIdValue = $('#khoaSelect').val(); // Lấy giá trị khoa từ dropdown
        const newService = {
            maDichVu: $('#dialog-add input[type="text"]').eq(1).val(),
            tenDichVu: $('#dialog-add input[type="text"]').eq(0).val(),
            donGia: parseFloat($('#dialog-add input[type="text"]').eq(2).val()),
            ngayTao: $('#dialog-add input[type="date"]').val(),
            moTaDichVu: $('#dialog-add input[type="text"]').eq(3).val(),
            khoaId: khoaIdValue === "" ? null : khoaIdValue // Nếu không chọn thì để null
        };

        axiosJWT.post('/api/Services', newService)
            .then(() => {
                loadServices(); // Tải lại danh sách
                $('#dialog-add').modal('hide'); // Đóng modal
            })
            .catch((error) => {
                showErrorPopup();
                console.error('Lỗi khi thêm dịch vụ:', error);
            });
    });

    // Sự kiện chỉnh sửa dịch vụ
    $(document).on('click', '.m-edit', function () {
        const serviceId = $(this).data('serviceId');
        const service = services.find(s => s.dichVuId === serviceId);

        if (!service) {
            alert('Không tìm thấy dịch vụ để chỉnh sửa.');
            return;
        }

        // Đổ dữ liệu vào modal chỉnh sửa
        $('#dialog-edit input[type="text"]').eq(0).val(service.tenDichVu);
        $('#dialog-edit input[type="text"]').eq(1).val(service.maDichVu);
        $('#dialog-edit input[type="text"]').eq(2).val(service.donGia);
        $('#dialog-edit input[type="date"]').eq(0).val(service.ngayTao.split('T')[0]);
        $('#dialog-edit input[type="text"]').eq(3).val(service.moTaDichVu);

        // Tải danh sách khoa và chọn khoa hiện tại
        loadKhoas('editKhoaSelect', service.khoaId);

        // Sự kiện sửa
        $('#btnEdit').off('click').on('click', function () {
            const khoaIdValue = $('#editKhoaSelect').val(); // Lấy giá trị từ dropdown
            const updatedService = {
                dichVuId: serviceId,
                maDichVu: service.maDichVu,
                tenDichVu: $('#dialog-edit input[type="text"]').eq(0).val(),
                donGia: parseFloat($('#dialog-edit input[type="text"]').eq(2).val()),
                ngayCapNhat: $('#dialog-edit input[type="date"]').eq(1).val(),
                moTaDichVu: $('#dialog-edit input[type="text"]').eq(3).val(),
                khoaId: khoaIdValue === "" ? null : khoaIdValue // Nếu không chọn thì để null
            };

            axiosJWT.put(`/api/Services/${serviceId}`, updatedService)
                .then(() => {
                    loadServices(); // Tải lại danh sách
                    $('#dialog-edit').modal('hide'); // Đóng modal
                })
                .catch((error) => {
                    showErrorPopup();
                    console.error('Lỗi khi chỉnh sửa dịch vụ:', error);
                });
        });
    });


    let selectedServiceId = null;
    // Sự kiện xóa dịch vụ
    $(document).on('click', '.m-delete', function () {
        selectedServiceId = $(this).data('service-id'); // Lấy ID dịch vụ từ nút
        const serviceName = $(this).closest('tr').find('td').eq(3).text(); // Tên dịch vụ từ cột thứ 4
        $('#dialog-confirm-delete .content').text(`Bạn có chắc chắn muốn xóa dịch vụ "${serviceName}"?`);
    });

    // Xử lý sự kiện khi xác nhận xóa trong modal
    $('#btnDelete').on('click', function () {
        if (!selectedServiceId) {
            alert('Không tìm thấy ID dịch vụ để xóa!');
            return;
        }

        // Gọi API xóa dịch vụ
        axiosJWT
            .delete(`/api/Services/${selectedServiceId}`)
            .then(() => {
                loadServices(); // Tải lại danh sách dịch vụ
            })
            .catch((error) => {
                showErrorPopup();
                console.error('Lỗi khi xóa dịch vụ:', error);
            })
            .finally(() => {
                selectedServiceId = null; // Reset ID sau khi xóa
            });
    });
});

// Hàm tải danh sách dịch vụ
function loadServices() {
    axiosJWT.get('/api/Services')
        .then((response) => {
            services = response.data;
            displayServices(services); // Hiển thị danh sách dịch vụ
        })
        .catch((error) => {
            console.error('Lỗi khi tải danh sách dịch vụ:', error);
        });
}

function loadKhoas(selectId, selectedKhoaId = null) {
    axiosJWT
        .get(`/api/v1/Departments`)
        .then(function (response) {
            const khoas = response.data;
            const khoaSelect = $(`#${selectId}`); // Lấy thẻ <select> từ HTML
            // Xóa danh sách cũ nếu có
            khoaSelect.empty();

            khoaSelect.append(`<option value="">Chọn khoa</option>`);
            khoas.forEach(khoa => {
                const isSelected = selectedKhoaId === khoa.khoaId ? 'selected' : '';
                const option = `<option value="${khoa.khoaId}" ${isSelected}>${khoa.tenKhoa}</option>`;
                khoaSelect.append(option); // Thêm từng khoa vào <select>
            });
        })
        .catch(function (error) {
            console.error("Lỗi khi lấy danh sách khoa:", error);
        });
}


// Hàm hiển thị danh sách dịch vụ
function displayServices(data) {
    const serviceTableBody = $('#tblData'); // Xác định phần tbody
    serviceTableBody.empty(); // Xóa nội dung cũ trước khi thêm mới

    if (data.length === 0) {
        serviceTableBody.append('<tr><td colspan="9">Không có dịch vụ nào.</td></tr>'); // Hiển thị thông báo nếu không có dữ liệu
        return;
    }

    // Lặp qua danh sách dịch vụ và tạo từng dòng
    data.forEach((service, index) => {
        const serviceRow = `
            <tr>
                <td class="chk"><input type="checkbox" /></td>
                <td empIdCell style="display: none">${service.dichVuId}</td>
                <td>${index + 1}</td>
                <td>${service.tenDichVu}</td>
                <td>${service.donGia.toLocaleString()}đ</td>
                <td>${service.moTaDichVu || "Không có mô tả"}</td>
                <td>${formatDate(service.ngayTao)}</td>
                <td>${formatDate(service.ngayCapNhat)}</td>
                <td>
                  <div class="m-table-tool">
                    <div class="m-edit m-tool-icon" data-service-id="${service.dichVuId}" data-bs-toggle="modal" data-bs-target="#dialog-edit">
                      <i class="fas fa-edit text-primary"></i>
                    </div>
                    <div class="m-delete m-tool-icon" data-service-id="${service.dichVuId}" data-bs-toggle="modal" data-bs-target="#dialog-confirm-delete">
                      <i class="fas fa-trash-alt text-danger"></i>
                    </div>
                  </div>
                </td>
            </tr>
        `;
        serviceTableBody.append(serviceRow); // Thêm dòng vào bảng
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

function getMaxDichVuCode(service) {
    let maxCode = 0;
    service.forEach(item => {
        const code = parseInt(item.maDichVu.replace('DV', '')); // Loại bỏ 'BN' và chuyển thành số
        if (code > maxCode) {
            maxCode = code;
        }
    });
    const nextCode = maxCode + 1;
    return 'DV' + nextCode.toString().padStart(3, '0');
}

// Hàm lọc danh sách dịch vụ theo từ khóa tìm kiếm
function filterServices(searchValue) {
    const filteredServices = services.filter(service =>
        removeAccents(service.tenDichVu.toLowerCase()).includes(searchValue)
    );
    displayServices(filteredServices); // Hiển thị danh sách sau khi lọc
}

// Hàm loại bỏ dấu tiếng Việt và chuyển thành chữ thường
function removeAccents(str) {
    return str
        .normalize("NFD") // Chuẩn hóa chuỗi Unicode
        .replace(/[\u0300-\u036f]/g, "") // Loại bỏ các dấu tiếng Việt
        .toLowerCase(); // Chuyển thành chữ thường
}