/* tslint:disable: member-ordering forin */
import { Component, OnInit }  from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AlertService } from '../../_shared/directives/alert-notification/alert.service';
import { PasswordValidator } from './password-validator';

import { UserProfileService } from '../user-profile/user-profile.service';
import { UserProfile } from '../user-profile/user-profile.model';

import { UserService } from '../user/user.service';
import { User } from '../user/user.model';

@Component({
    selector: 'user-detail',
    templateUrl: 'user-detail.component.html',
    providers: [UserService, UserProfileService, AlertService]
})
export class UserDetailComponent{
 title: string = "Users";
    userForm: FormGroup;
    user: User;
    userId: string = '';
    customerId: string = '';
    owners: User[];   
    profiles: UserProfile[];
        
    constructor(
        private fb: FormBuilder, 
        private userService: UserService,
        private userProfileService: UserProfileService,
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location        
    ){}

    ngOnInit(): void {        
        this.alertService.clear();

        this.route.params.subscribe((params: Params) => {
            this.userId = params['id'];            
        });
                
        this.loadUserProfiles();

        if(this.userId != undefined)
        {            
            this.loadUser();               
        }else{
            this.user = new User();
            this.userId = undefined;
            this.buildForm();    
            this.startErrors();           
        }    
    }
    
    loadUser(){
        this.userService.getById(this.userId)
            .subscribe(
                response =>{
                    this.user = (response.Data as any);  
                    this.user.Password = "";                      
                    this.buildForm();          
                    this.userForm.get("Username").disable();                
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    conditionalRequired() {
        return (control: FormControl): { [s: string]: boolean } => {
            let validate: boolean = true;

            if(this.userId != undefined ){
                validate = false;
            }
            
            if (validate) {
                return { required: true };
            }
        }
    }

    buildForm(): void {
        this.userForm = this.fb.group({
            'FirstName': [
                this.user.FirstName, [
                    Validators.required
                ]
            ],
            'LastName': [
                this.user.LastName, [
                    Validators.required
                ]
            ],
            'Username': [
                this.user.Username, [                    
                    Validators.required,                    
                ]
            ],
            'Password': [
                this.user.Password, [
                    Validators.compose([this.conditionalRequired()]),
                    Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,12})/)
                ]
            ],
            'ConfirmPassword': [
                this.user.ConfirmPassword, []
            ],
            'Email': [
                this.user.Email, [
                    Validators.required,
                    Validators.email
                ]
            ],
            'UserProfileId': [
                this.user.UserProfileId, [
                    Validators.required
                ]
            ]
        },{
            validator: PasswordValidator.MatchPassword
        });

        this.userForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
        
    }

   onValueChanged(data?: any) {
        if (!this.userForm) { return; }
        const form = this.userForm;

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

    addNew(){
        this.router.navigate(['/user-detail']);
    }

    update(){
        this.user.UserId = parseInt(this.userId);
        this.userService.update(this.user)
            .subscribe(
                response =>{
                    this.router.navigate(['/user-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });
    }

    insert() {
        this.userService.insert(this.user)
            .subscribe(
                response =>{
                    this.router.navigate(['/user-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });        
    }

    onSubmit() {
        this.alertService.clear();

        this.user = this.userForm.value;        
        this.user.CustomerId = parseInt(sessionStorage.getItem("selectedCustomerId"));
        
        if(this.userId != undefined){
            this.update();
        }else{
            this.insert();
        }
    }

    goBack(){
        this.location.back();
    }

    loadUserProfiles(){
        
        this.userProfileService.getAll().
                subscribe(
                    response =>{
                        this.profiles = (response.Data as any);                                 
                    }, err =>{
                        this.alertService.error(err.message);
                    });
    }

    startErrors() {
        this.formErrors.FirstName = 'FirstName required',
        this.formErrors.LastName = 'LastName required',
        this.formErrors.Username = 'Username required',
        this.formErrors.Password = 'Password required',
        this.formErrors.Email = 'Email required',
        this.formErrors.UserProfileId = 'Profile required'
    }

    formErrors = {
        'FirstName': '',
        'LastName': '',
        'Username': '',
        'Password': '',
        'Email': '',
        'UserProfileId' : ''
    };

    validationMessages = {
        'FirstName': {
            'required': 'First Name required.'
        },
        'LastName': {
            'required': 'Last Name required.'
        },
        'Username': {
            'required': 'Username required.'
        },
        'Password': {
            'required': 'Password required.',
            'pattern' : 'Password must contains at least 8 characters, one capital letter, one small letter, numbers and special characters'
        },
        'Email': {
            'required': 'Email required.',
            'email': "Email invalid"
        },
        'UserProfileId': {
            'required': 'Profile required.'
        }
    };
}