/* Angular Modules */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpModule, JsonpModule } from "@angular/http";

/* Custom Directives */
import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { TestCaseService } from './test-case.service';

@NgModule({
    imports: [
        HttpModule, JsonpModule,
        AuthModule        
    ]    
})
export class TestCaseModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: TestCaseModule,
            providers: [TestCaseService]
        }
    }    
}
