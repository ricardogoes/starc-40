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

import { ProjectService } from './project.service';
import { ProjectDetailComponent } from './project-detail.component';
import { ProjectListComponent } from './project-list.component';

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
    declarations: [ProjectDetailComponent, ProjectListComponent]
})
export class ProjectModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: ProjectModule,
            providers: [ProjectService]
        }
    }    
}
