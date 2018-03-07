import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { By }              from '@angular/platform-browser';
import { DebugElement }    from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Http } from "@angular/http";

import { AuthModule } from '../../_shared/services/authentication/authentication.module';
import { AuthService } from '../../_shared/services/authentication/authentication.service';

import { AlertNotificationModule } from '../../_shared/directives/alert-notification/alert-notification.module';
import { LoginComponent } from './login.component';

describe('Component: Login', () => {

    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    let debug: DebugElement;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [ReactiveFormsModule, FormsModule, AlertNotificationModule, AuthModule],
            declarations: [ LoginComponent ],
            providers: [ AuthService ]
        });

        fixture = TestBed.createComponent(LoginComponent);        
        component = fixture.componentInstance;
        component.ngOnInit();

        var authService = fixture.debugElement.injector.get(AuthService);

        // query for the title <h1> by CSS element selector
        debug = fixture.debugElement.query(By.css('h5'));
        element = debug.nativeElement;
    }));

    /*it('should display original title', () => {
        fixture.detectChanges();
        expect(element.textContent).toContain(component.title);
    });*/
});