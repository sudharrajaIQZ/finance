import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom, Observable, retry } from 'rxjs';
import { AuthDto, RegisterDto, VerifyOtp } from '../models/auth.dto';
import { environment } from '../environment/environment';
import { CustomerDto } from '../models/customer.dto';
import { TransactionDto } from '../models/transaction.dto';
@Injectable({
  providedIn: 'root'
})
export class Service {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // login
  login(authData: AuthDto) {
    const request$ = this.http.post(`${this.apiUrl}/user/login`, authData);
    const result = lastValueFrom(request$) as any;
    return result;
  }

  // register
  register(registerData: RegisterDto) {
    const request$ = this.http.post(`${this.apiUrl}/user`, registerData);
    const result = lastValueFrom(request$) as any;
    return result;
  }
  
  verifyotp(verifyOtp: VerifyOtp) {
    const request$ = this.http.post(`${this.apiUrl}/user/verify-otp`, verifyOtp);
    const result = lastValueFrom(request$) as any;
    return result;
  }

  saveCustomer(customerData: CustomerDto) {
    const request$ = this.http.post(`${this.apiUrl}/customer`, customerData);
    const result = lastValueFrom(request$) as any
    return result;
  }

  getCustomer() {
    const request$ = this.http.get(`${this.apiUrl}/customer`);
    const result = lastValueFrom(request$) as any;
    return result
  }

  Payment(transactionData: TransactionDto) {
    const request$ = this.http.post(`${this.apiUrl}/transaction`, transactionData)
    const result = lastValueFrom(request$) as any
    return result;
  }
  getTransactionList() {
    const request$ = this.http.get(`${this.apiUrl}/transaction`);
    const result = lastValueFrom(request$) as any;
    console.log(result, "Transaction List:");
    return result;
  }

}
