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
        private readonly IConfiguration _configuration;
        private readonly IJwtInterface _jwtInterface;

        public AuthService(AppDbContext appDb, IConfiguration configuration, IJwtInterface jwtInterface)
        {
            this.appDbContext = appDb;
            this._configuration = configuration;
            this._jwtInterface = jwtInterface;
        }

        public async Task <IEnumerable<AuthModel>> getUsersAsync()
        {
            var userList = await appDbContext.users.ToListAsync();
            return userList;
        }

        public async Task<BaseResponse<object>> createUsersAsync(CreateUsers createUsers)
        {
            // Check the guest role to get id
            var userRole = await appDbContext.roles.FirstOrDefaultAsync(r => r.Name == "Guest");
            if (userRole == null)
            {
                return new BaseResponse<object>("Default 'Guest' role not found", (int)HttpStatusCode.InternalServerError);
            }

            Random RandomOtp = new Random();

            var newAuth = new AuthModel
            {
                Id = Guid.NewGuid(),
                UserName = createUsers.UserName,
                EmailId = createUsers.EmailId,
                Password = createUsers.Password,
                isVerified = false,
                Otp = RandomOtp.Next(100000, 999999),
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };

            await appDbContext.users.AddAsync(newAuth);
            await appDbContext.SaveChangesAsync();

            var newUserRole = new UserRole
            {
                UserId = newAuth.Id,
                RoleId = userRole.Id
            };

            await appDbContext.userRoles.AddAsync(newUserRole);
            await appDbContext.SaveChangesAsync();

            return new BaseResponse<object>("User Created Successfully with 'Guest' role", (int)HttpStatusCode.Created, new { Otp = newAuth.Otp });
        }


        public async Task<BaseResponse<object>> updateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var findUser = await appDbContext.users.FirstOrDefaultAsync(e => e.Id == id);
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
            var findDeleteUser = await appDbContext.users.FirstOrDefaultAsync(e => e.Id == id);
            if (findDeleteUser != null)
            {
                appDbContext.users.Remove(findDeleteUser);
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
            var user = await appDbContext.users
                .FirstOrDefaultAsync(u => u.EmailId == loginDto.EmailId);

            if (user == null || user.Password != loginDto.Password)
            {
                return new BaseResponse<object>("Invalid Email or Password", (int)HttpStatusCode.Unauthorized);
            }
            var tokens = _jwtInterface.GenerateToken(user);

            // save refresh token
            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(3);
            
            await appDbContext.SaveChangesAsync();
            var userData = new
            {
                user.Id,
                user.EmailId,
                AccessToken=tokens.AccessToken,
                RefreshToken=tokens.RefreshToken
            };

            return new BaseResponse<object>("Login successful", (int)HttpStatusCode.OK, userData);
        }


        public async Task<BaseResponse<object>> verifyOtpAsync(string Email, verifyOtp verifyOtp)
        {
            var otpVerify = await appDbContext.users.FirstAsync(e => e.EmailId == Email);
            if (otpVerify == null) return new BaseResponse<object>("Email not found", (int)HttpStatusCode.Unauthorized);

            if (otpVerify.Otp != verifyOtp.Otp) return new BaseResponse<object>("OTP is invalid", (int)HttpStatusCode.NotAcceptable);

            otpVerify.isVerified = true;
            await appDbContext.SaveChangesAsync();

            return new BaseResponse<object>("Otp verified succesfully", (int)HttpStatusCode.Created);
        }
    }
}
