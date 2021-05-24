import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerInfoComponent} from './Info-Components/customer-info/customer-info.component';
import {ProductInfoComponent} from './Info-Components/product-info/product-info.component';
import { EmployeeInfoComponent } from './Info-Components/employee-info/employee-info.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    EmployeeInfoComponent,
    CustomerInfoComponent,
    ProductInfoComponent,
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    EmployeeInfoComponent,
    CustomerInfoComponent,
    ProductInfoComponent,
  ]
})
export class ComponentsModule { }
