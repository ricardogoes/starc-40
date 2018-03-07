import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

/* App Root */
import { AppComponent }   from './app.component';

/* Layout Components */
import { PublicComponent } from './layouts/public/public.component'
import { SecureComponent } from './layouts/secure/secure.component'

/* DataGrid */
//import { PaginationComponent } from './directives/datagrid/pagination.component';

/* Feature Modules */
//import { FilterModule }    from './filters/filter.module';
import { LoginModule }       from './public/login/login.module';
import { AuthModule }       from './_shared/services/authentication/authentication.module';
import { AlertNotificationModule} from './_shared/directives/alert-notification/alert-notification.module';
import { CustomerModule }       from './secure/customer/customer.module';
import { HomeModule }       from './secure/home/home.module';
import { ProjectModule }       from './secure/project/project.module';
import { UserModule }       from './secure/user/user.module';
import { UsersInProjectsModule }       from './secure/user-project/user-project.module';
import { TestPlanModule }       from './secure/test-plan/test-plan.module';
import { ManageTestsModule }       from './secure/manage-tests/manage-tests.module';

/* Routing Module */
import { AppRoutes } from './app.routes';

@NgModule({
    imports: [
        BrowserModule,
        CommonModule,
        FormsModule, ReactiveFormsModule,
        HttpModule, JsonpModule,
        //FilterModule,
        LoginModule,
        AlertNotificationModule,
        AuthModule.forRoot(),        
        HomeModule,
        CustomerModule.forRoot(),
        ProjectModule.forRoot(),
        UserModule.forRoot(),
        UsersInProjectsModule.forRoot(),
        TestPlanModule.forRoot(),
        ManageTestsModule,
        AppRoutes
    ],
    declarations: [ AppComponent, PublicComponent, SecureComponent/*, PaginationComponent */],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }
