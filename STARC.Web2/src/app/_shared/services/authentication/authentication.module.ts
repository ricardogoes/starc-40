import { NgModule, ModuleWithProviders } from '@angular/core';

import { AuthGuard } from './authentication.guard';
import { AuthService } from './authentication.service';

@NgModule({})
export class AuthModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: AuthModule,
            providers: [AuthGuard, AuthService]
        }
    }
}
