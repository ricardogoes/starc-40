import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { DataGridModule } from '../../_shared/directives/datagrid/datagrid.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { UsersInProjectsComponent } from './user-project.component';
import { UsersInProjectsService } from './user-project.service';

import { ProjectModule } from '../project/project.module';
import { UserModule} from '../user/user.module';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule, JsonpModule,
        AuthModule, 
        DataGridModule,
        AlertNotificationModule,
        ProjectModule.forRoot(),
        UserModule.forRoot()
    ],
    declarations: [UsersInProjectsComponent]
})
export class UsersInProjectsModule {
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: UsersInProjectsModule,
            providers: [UsersInProjectsService]
        }
    }
 }
