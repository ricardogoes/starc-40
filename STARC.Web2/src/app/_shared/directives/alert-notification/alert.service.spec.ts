import {} from 'jasmine';
import { TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { AlertService } from './alert.service';

describe('Directive: Alert Notification', () =>
{
    
    beforeEach(() =>
    {
        TestBed.configureTestingModule({
            imports: [ RouterTestingModule.withRoutes([]) ],
            providers: [ AlertService ]
        });
    });
    
    it('Should validate that success method call alert method', inject([AlertService], (alertService: AlertService) =>
    {
        spyOn(alertService, "alert");
        
        alertService.success("Unit Test", false);
        //alertService.clear();

        expect(alertService.alert).toHaveBeenCalled();        
    }));

    it('Should validate that error method call alert method', inject([AlertService], (alertService: AlertService) =>
    {
        spyOn(alertService, "alert");
        
        alertService.error("Unit Test", false);
        //alertService.clear();

        expect(alertService.alert).toHaveBeenCalled();        
    }));

    it('Should validate that info method call alert method', inject([AlertService], (alertService: AlertService) =>
    {
        spyOn(alertService, "alert");
        
        alertService.info("Unit Test", false);
        //alertService.clear();

        expect(alertService.alert).toHaveBeenCalled();        
    }));

    it('Should validate that warn method call alert method', inject([AlertService], (alertService: AlertService) =>
    {
        spyOn(alertService, "alert");
        
        alertService.warn("Unit Test", false);
        //alertService.clear();

        expect(alertService.alert).toHaveBeenCalled();        
    }));
});