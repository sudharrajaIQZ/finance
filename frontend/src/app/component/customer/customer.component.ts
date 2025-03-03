import { Component } from '@angular/core';
import { CustomerDto } from '../../models/customer.dto';
import { Service } from '../../service/service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [HttpClientModule, CommonModule, FormsModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css',
  providers: [Service]

})
export class CustomerComponent {
  customerData: CustomerDto = new CustomerDto();
  constructor(private service: Service) { };
  responseMessage: string = '';
  isError: boolean = false;

  async onSubmit() {
    try {
      const response = await this.service.saveCustomer(this.customerData);
      return response;
      } catch (error) {
      console.log(error);
    }
  }

}
