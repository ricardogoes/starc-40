import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { FilterModule } from '../../_shared/filters/filter.module';
import { DataGridModule } from '../../_shared/directives/datagrid/datagrid.module';
import { CpfCnpjValidatorModule } from '../../_shared/directives/cpf-cnpj-validator/cpf-cnpj-validator.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { CustomerService } from './customer.service';
import { CustomerDetailComponent } from './customer-detail.component';
import { CustomerListComponent } from './customer-list.component';

@NgModule({
    imports: [
        BrowserModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule, JsonpModule,
        AuthModule,
        FilterModule,
        DataGridModule ,
        CpfCnpjValidatorModule ,
        AlertNotificationModule     
    ],
    declarations: [CustomerDetailComponent, CustomerListComponent]

})
export class CustomerModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: CustomerModule,
            providers: [CustomerService]
        }
    }
}
