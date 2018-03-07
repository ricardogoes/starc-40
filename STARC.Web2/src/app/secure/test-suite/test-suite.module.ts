/* Angular Modules */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpModule, JsonpModule } from "@angular/http";

/* Custom Directives */
import { AuthModule } from '../../_shared/services/authentication/authentication.module';

import { TestSuiteService } from './test-suite.service';

@NgModule({
    imports: [
        HttpModule, JsonpModule,
        AuthModule        
    ]    
})
export class TestSuiteModule { 
    static forRoot() : ModuleWithProviders{
        return {
            ngModule: TestSuiteModule,
            providers: [TestSuiteService]
        }
    }    
}
