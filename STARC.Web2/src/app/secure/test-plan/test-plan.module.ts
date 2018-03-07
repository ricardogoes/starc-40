/* Angular Modules */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

/* Custom Directives */
import { DateValueAccessorModule } from '../../_shared/directives/date-value-accessor/data-value.module';
import { FilterModule } from '../../_shared/filters/filter.module';
import { DataGridModule } from '../../_shared/directives/datagrid/datagrid.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { TestPlanService } from './test-plan.service';
import { TestPlanDetailComponent } from './test-plan-detail.component';
import { TestPlanListComponent } from './test-plan-list.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule, JsonpModule,
        AuthModule,
        DataGridModule,
        FilterModule,
        DateValueAccessorModule,
        AlertNotificationModule
    ],
    declarations: [TestPlanDetailComponent, TestPlanListComponent]
})
export class TestPlanModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: TestPlanModule,
            providers: [TestPlanService]
        }
    }    
}
