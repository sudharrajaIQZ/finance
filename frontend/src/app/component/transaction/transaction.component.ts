import { Component } from '@angular/core';
import { TransactionDto } from '../../models/transaction.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Service } from '../../service/service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CustomerDto } from '../../models/customer.dto';
@Component({
  selector: 'app-transaction',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './transaction.component.html',
  styleUrl: './transaction.component.css',
  providers: [Service]
})

export class TransactionComponent {
  customerList: CustomerDto[] = [];
  transactionList: TransactionDto[] = [];
  //
  res: TransactionDto = new TransactionDto();

  constructor(private service: Service) { }

  async ngOnInit(): Promise<void> {
    await this.getCustomers();
    await this.getTransactionList();
  }

  /** Fetch customer list */
  async getCustomers() {
    try {
      const res = await this.service.getCustomer();
      this.customerList = res.data;
    } catch (error) {
      console.error("Error fetching customers:", error);
    }
  }

  /** Fetch transaction list */
  async getTransactionList() {
    try {
      const res = await this.service.getTransactionList();
      this.transactionList = res;
    } catch (error) {
      console.error("Error fetching transactions:", error);
    }
  }

  // transaction list for each 
  async onSubmit() {
    try {
      if (this.res) {
        const response = await this.service.Payment(this.res);
      }
    } catch (error) {
      console.log("Transaction failed:", error);
    }
  }
}
