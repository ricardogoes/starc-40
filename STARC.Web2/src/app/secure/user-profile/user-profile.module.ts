import { NgModule, ModuleWithProviders } from '@angular/core';

import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { UserProfileService } from './user-profile.service';

@NgModule({
    imports: [
       
    ],
    declarations: []
})
export class UserProfileModule {
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: UserProfileModule,
            providers: [UserProfileService]
        }
    }
 }
