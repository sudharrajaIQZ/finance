using FluentValidation;
using System.Data;

namespace backend.Dto
{
    public class CreateUsers
        {
            public string UserName { get; set; }
            public string EmailId { get; set; }
            public string Password { get; set; }
        }
    public class CreateUserValidator : AbstractValidator<CreateUsers>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.EmailId).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(16).WithMessage("Password must be at least 8 characters long");
        }
    }

    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("userName is Required");
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("EmailID is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be at least 6 character long");
        }
    }
    public class LoginDto
    {
        public string EmailId { get; set; }
        public string Password { get; set; }    
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("Invalid EmailId");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Invalid Password");

        }
    }
    public class verifyOtp
    {
        public int Otp { get; set; }
    }

}
