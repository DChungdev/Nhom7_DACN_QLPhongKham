$('#loginForm').on('submit', function(e) {
    e.preventDefault(); // Ngăn chặn form tự động submit

    var username = $('#username').val();
    var password = $('#password').val();

    $.ajax({
        url: 'http://localhost:37649/api/Auth/login', // URL của API đăng nhập
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ username: username, password: password }),
        success: function(response) {
            // Lưu accessToken và refreshToken vào localStorage
            localStorage.setItem('accessToken', response.token);
            localStorage.setItem('refreshToken', response.refreshToken);
            console.log(response);
            let token = response.token;
            var userRole;
            if (token) {
                try {
                    let decodedToken = jwt_decode(token);
                    userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                    localStorage.setItem('userName', username);
                    console.log(decodedToken); // In ra để kiểm tra thông tin token
                } catch (error) {
                    console.error('Token không hợp lệ:', error);
                }
            } else {
                console.error('Token rỗng hoặc không hợp lệ.');
            }

            // Kiểm tra role và chuyển hướng
            if (userRole === 'Admin') {
                window.location.href = '/Admin/MainAdmin.html'; // Chuyển hướng admin
                console.log(username);
                $("#displayUser").text(username);
            } 
            else if (userRole === 'Patient') {
                window.location.href = '/User/index.html'; // Chuyển hướng user
            }
            else if (userRole === 'Doctor') {
                window.location.href = '/User/index.html';
            }
        },
        error: function(xhr) {
            // Xử lý lỗi đăng nhập
            alert('Đăng nhập không thành công: ' + xhr.responseText);
        }
    });
});

$('#registerForm').on('submit', function(e) {
    e.preventDefault(); // Ngăn chặn form tự động submit

    var email = $('#email').val();
    var username = $('#username').val();
    var password = $('#password').val();

    if (!username || !email || !password) {
        alert("Hãy điền đầy đủ thông tin!");
        return;
    }

    $.ajax({
        url: 'http://localhost:37649/api/Auth/register', // URL của API đăng nhập
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ username: username, email: email, password: password }),
        success: function(response) {
            // Xử lý khi đăng ký thành công
            if(response.Status == "Success"){
                console.log('Đăng ký thành công:', response);
                alert('Đăng ký thành công!');

                 // Chuyển hướng tới trang đăng nhập hoặc trang khác
                window.location.href = "authentication-login1.html";
            }
            console.log('Đăng ký thất bại:', response.message);
                alert('Đăng ký thất bại! ' + response.message);
        },
        error: function(xhr) {
            try {
                var response = JSON.parse(xhr.responseText);
                var errorMessage = response.message || "Đã xảy ra lỗi!";
                alert(errorMessage);
            } catch (e) {
                alert("Đã xảy ra lỗi không xác định. Vui lòng thử lại.");
            }
        }
    });
});
