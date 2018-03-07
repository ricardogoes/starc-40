import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { AuthModule } from '../../_shared/services/authentication/authentication.module';
import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';

import { LoginComponent } from './login.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule, JsonpModule,
    AuthModule,
    AlertNotificationModule
  ],
  declarations: [ LoginComponent ]
})
export class LoginModule { }
