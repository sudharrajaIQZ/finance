using backend.Models;
using backend.Dto;

namespace backend.Interface
{
    public interface IAuthInterface
    {
        Task<IEnumerable<AuthModel>> getUsersAsync();
        Task<BaseResponse<object>> createUsersAsync(CreateUsers createUsers);
        Task<BaseResponse<object>> updateUserAsync(Guid id, UpdateUserDto updateUserDto);
        Task<BaseResponse<object>> deletUserAsync(Guid id);
        Task<BaseResponse<object>> LoginAsync(LoginDto loginDto);
        Task<BaseResponse<object>> verifyOtpAsync(string Email, verifyOtp verifyOtp);
        
    }
}
