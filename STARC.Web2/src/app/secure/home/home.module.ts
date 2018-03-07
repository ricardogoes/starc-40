import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from "@angular/http";

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { HomeComponent } from './home.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule, JsonpModule,
        AuthModule
    ],
    declarations: [HomeComponent]
})
export class HomeModule { }
