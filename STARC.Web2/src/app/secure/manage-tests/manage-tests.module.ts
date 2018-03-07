import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { FilterModule } from '../../_shared/filters/filter.module';
import { DataGridModule } from '../../_shared/directives/datagrid/datagrid.module';
import { CpfCnpjValidatorModule } from '../../_shared/directives/cpf-cnpj-validator/cpf-cnpj-validator.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { AuthModule } from '../../_shared/services/authentication/authentication.module';
import { TreeModule } from '../../_shared/directives/ng2-tree';

import { TestCaseService } from '../test-case/test-case.service';
import { TestSuiteService } from '../test-suite/test-suite.service';

import { ManageTestsComponent } from './manage-tests.component';

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
        AlertNotificationModule,
        TreeModule     
    ],
    declarations: [ManageTestsComponent],
    providers: [TestCaseService, TestSuiteService]

})
export class ManageTestsModule { 
   
}
