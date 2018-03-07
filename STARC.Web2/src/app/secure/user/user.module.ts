import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { FilterModule } from '../../_shared/filters/filter.module';
import { DataGridModule } from '../../_shared/directives/datagrid/datagrid.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { UserProfileModule } from '../user-profile/user-profile.module';

import { UserDetailComponent } from './user-detail.component';
import { UserListComponent } from './user-list.component';
import { UserService } from './user.service';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule, JsonpModule,
        AuthModule, 
        FilterModule,
        DataGridModule,
        AlertNotificationModule,
        UserProfileModule
    ],
    declarations: [UserDetailComponent, UserListComponent]
})
export class UserModule {
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: UserModule,
            providers: [UserService]
        }
    }
 }
