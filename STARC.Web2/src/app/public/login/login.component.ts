import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from "../../_shared/services/authentication/authentication.service";
import { AlertService } from "../../_shared/directives/alert-notification/alert.service";
import { UserLogin } from './login.model';

@Component({
    selector: "login",
    templateUrl: 'login.component.html',
    providers: [ AuthService, AlertService ]
})
export class LoginComponent implements OnInit{
    title: string = "Login";
    loginForm: FormGroup;
    userLogin: UserLogin;
    returnUrl: string = "";

    constructor(
        private fb: FormBuilder, 
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.alertService.clear();

        // reset login status
        this.authService.logout();
 
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

        this.userLogin = new UserLogin();
        this.startErrors();

        this.buildForm();
    }

    buildForm(): void {
        this.loginForm = this.fb.group({
            'Username': [
                this.userLogin.Username, [
                    Validators.required
                ]
            ],
            'Password': [
                this.userLogin.Password, [
                    Validators.required
                ]
            ]
        });

        this.loginForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
    }

    onValueChanged(data?: any) {
        if (!this.loginForm) { return; }
        const form = this.loginForm;

        for (const field in this.formErrors) {
            const control = form.get(field);

             // clear previous error message (if any)
            if(control.valid)
                this.formErrors[field] = '';

            if (control && control.dirty && !control.valid) {
                const messages = this.validationMessages[field];
                for (const key in control.errors) {
                    
                    this.formErrors[field] = messages[key] + ' ';
                }
            }
        }
    }

    onSubmit() {
        this.alertService.clear();

        this.userLogin = this.loginForm.value;

        this.authService.login(this.userLogin.Username, this.userLogin.Password)
            .subscribe(result => {
                if (result.State == 1) {
                    if(this.returnUrl != "/"){
                        this.router.navigate([this.returnUrl]);
                    }else{                        
                        this.router.navigate(["./home"]);                        
                    }
                }
                else {
                    this.alertService.error(result.Message);
                }
            });
    }

    startErrors(){
        this.formErrors.Username = "Username is required";     
        this.formErrors.Password = "Password is required";     
    }

    formErrors = {
        'Username': '',
        'Password': ''
    };

    validationMessages = {
        'Username': {
            'required': 'Username is required.'
        },
        'Password': {
            'required': 'Password is required.'
        }       
    };
}
