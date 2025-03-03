import { Routes } from '@angular/router';
import { DashboardComponent } from './component/dashboard/dashboard.component';
import { HeaderComponent } from './component/header/header.component';
import { AuthComponent } from './component/auth/auth.component';
import { CustomerComponent } from './component/customer/customer.component';
import { TransactionComponent } from './component/transaction/transaction.component';

export const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'header', component: HeaderComponent },
    { path: 'auth/login', component: AuthComponent },
    { path: 'transaction', component: TransactionComponent },
    { path: 'customer', component: CustomerComponent }
];
