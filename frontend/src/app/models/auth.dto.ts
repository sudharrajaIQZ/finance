
export class AuthDto {
    emailId: string = "";
    password: string = "";
}

export class RegisterDto {
    userName: string = '';
    emailId: string = '';
    password: string = '';
}

export class VerifyOtp {
    Email: string = "";
    otp: number = 0;
}
