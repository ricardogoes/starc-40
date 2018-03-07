import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AlertComponent } from './alert.component'
import { AlertService } from './alert.service'

@NgModule({
    imports: [
        BrowserModule
    ],
    declarations: [ AlertComponent ],
    exports: [ AlertComponent ]
})
export class AlertNotificationModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: AlertNotificationModule,
            providers: [ AlertService ]
        }
    }    
}
