import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Service } from '../../service/service';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthDto, RegisterDto, VerifyOtp } from '../../models/auth.dto';


@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  providers: [Service],
  templateUrl: './auth.component.html',
  styleUrls: []
})
export class AuthComponent {
  authData: AuthDto = new AuthDto();
  registerData: RegisterDto = new RegisterDto();
  verifyOtp: VerifyOtp = new VerifyOtp();

  responseMessage: string = '';
  isError: boolean = false;
  isLoginMode: boolean = true;
  isVerification: boolean = false;

  constructor(private service: Service, private router: Router) { }

  async onSubmit() {
    this.responseMessage = '';
    this.isError = false;
    if (this.isLoginMode && !this.isVerification) {
      try {
        const response = await this.service.login(this.authData);
        console.log(response);
        this.responseMessage = response.message;        
        if (response.code == 201) {
          this.router.navigate([''])
        }
      } catch (ex: any) {
        this.isError = true
        this.responseMessage = ex.message;
      } finally {
        setTimeout(() => {
          this.isError = false;
          this.responseMessage = '';
        })
      }
    } else if (!this.isLoginMode && !this.isVerification) {
      try {
        const response = await this.service.register(this.registerData);
        this.responseMessage = response.message;
        this.isVerification = true;
      } catch (error: any) {
        this.isError = true
        this.responseMessage = error.message;
      }
    } else {
      try {
        const response = await this.service.verifyotp(this.verifyOtp);
        if (response.code == 201) {
          this.router.navigate(['/'])
        }
      } catch (error) {
        console.log(error);
      }
    }
  }

  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    this.responseMessage = '';
  }
}
