using backend.DataBase;
using backend.Dto;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        private readonly IJwtInterface _jwtInterface;

        public AuthController(IAuthInterface authInterface,IJwtInterface jwtInterface)
        {
            this._authInterface = authInterface;
            this._jwtInterface = jwtInterface;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getUsers()
        {
            var users = await _authInterface.getUsersAsync();
            var response = new BaseResponse<object>("User list fetched successfully", (int)HttpStatusCode.OK, users);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createUser([FromBody] CreateUsers createUsers)
        {
            var createUser = await _authInterface.createUsersAsync(createUsers);
            return StatusCode(createUser.Code, createUser);
        }

        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> updateUser([FromRoute] Guid id, UpdateUserDto updateUserDto)
        {
            var updateUser = await _authInterface.updateUserAsync(id, updateUserDto);
            return Ok(updateUser);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteUser([FromRoute] Guid id)
        {
            var deleteUser = await _authInterface.deletUserAsync(id);
            return Ok(deleteUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var loginResponse = await _authInterface.LoginAsync(loginDto);

            if (loginResponse.Code == (int)HttpStatusCode.OK && loginResponse.Data is not null)
            {
                return Ok(loginResponse);
            }

            return Unauthorized(new BaseResponse<object>(loginResponse.Message, loginResponse.Code));
        }


        [HttpPost]
        [Route("verify-otp")]
        public async Task<IActionResult> Otpverify([FromBody] verifyOtp verifyOtp,string Email)
        {
            var otpVerification = await _authInterface.verifyOtpAsync(Email, verifyOtp);
            return Ok();
        }
    }
}
