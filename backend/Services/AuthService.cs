using backend.DataBase;
using backend.Dto;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace backend.Services
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext appDbContext;

        public AuthService(AppDbContext appDb)
        {
            this.appDbContext = appDb;
        }

        public async Task <IEnumerable<AuthModel>> getUsersAsync()
        {
            var userList = await appDbContext.userDetails.ToListAsync();
            return userList;
        }

        public async Task<BaseResponse<object>> createUsersAsync(CreateUsers createUsers)
        {
            Random RandomOtp = new Random();
            var newAuth = new AuthModel
            {
                Id = new Guid(),
                UserName = createUsers.UserName,
                EmailId = createUsers.EmailId,
                Password = createUsers.Password,
                isVerified = false,
                Otp = RandomOtp.Next(100000, 999999),

            };

            await appDbContext.userDetails.AddAsync(newAuth);
            await appDbContext.SaveChangesAsync();

            return new BaseResponse<object>("User Created Successfully", (int)HttpStatusCode.Created, new { Otp = newAuth.Otp });

        }

        public async Task<BaseResponse<object>> updateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var findUser = await appDbContext.userDetails.FirstOrDefaultAsync(e => e.Id == id);
            if (findUser != null)
            {
                findUser.UserName = updateUserDto.UserName;
                findUser.EmailId = updateUserDto.EmailId;
                findUser.Password = updateUserDto.Password;
                findUser.updatedAt = DateTime.UtcNow;

                await appDbContext.SaveChangesAsync();

                return new BaseResponse<object>("User Updated successfully", (int)HttpStatusCode.Created);
            }
            else
            {
                return new BaseResponse<object>("User Not Updated", (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<BaseResponse<object>> deletUserAsync(Guid id)
        {
            var findDeleteUser = await appDbContext.userDetails.FirstOrDefaultAsync(e => e.Id == id);
            if (findDeleteUser != null)
            {
                appDbContext.userDetails.Remove(findDeleteUser);
                await appDbContext.SaveChangesAsync();
                return new BaseResponse<object>("User Deleted Successfully", (int)HttpStatusCode.Created);

            }
            else
            {
                return new BaseResponse<object>("User id Not found", (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<BaseResponse<object>> LoginAsync(LoginDto loginDto)
        {
            var user = await appDbContext.userDetails.FirstOrDefaultAsync(u => u.EmailId == loginDto.EmailId && u.Password == loginDto.Password);
            if (user != null)
            {
                return new BaseResponse<object>("Login succesfully", (int)HttpStatusCode.Created);
            }
            else
            {
                return new BaseResponse<object>("Invalid Email and Password", (int)HttpStatusCode.Unauthorized);
            }
        }

        public async Task<BaseResponse<object>> verifyOtpAsync(string Email, verifyOtp verifyOtp)
        {
            var otpVerify = await appDbContext.userDetails.FirstAsync(e => e.EmailId == Email);
            if (otpVerify == null) return new BaseResponse<object>("Email not found", (int)HttpStatusCode.Unauthorized);

            if (otpVerify.Otp != verifyOtp.Otp) return new BaseResponse<object>("OTP is invalid", (int)HttpStatusCode.NotAcceptable);

            otpVerify.isVerified = true;
            await appDbContext.SaveChangesAsync();

            return new BaseResponse<object>("Otp verified succesfully", (int)HttpStatusCode.Created);
        }
    }
}
