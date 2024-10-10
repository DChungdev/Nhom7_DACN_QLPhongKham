using QuanLyPhongKham.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKham.Business.Interfaces
{
    public interface IAuthService
    {
        Task<object> LoginAsync(LoginModel model);
        Task<object> RegisterAsync(RegisterModel model);
        Task<Response> RefreshTokenAsync(TokenModel tokenModel);
        Task<string> Revoke(string username);
        Task<string> RevokeAll();
        
    }
}
