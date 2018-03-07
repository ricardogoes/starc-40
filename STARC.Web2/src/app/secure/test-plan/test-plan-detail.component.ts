/* tslint:disable: member-ordering forin */
import { Component, OnInit }  from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

import { TestPlanService } from './test-plan.service';
import { TestPlan } from './test-plan.model';

import { UserService } from '../user/user.service';
import { User } from '../user/user.model';

@Component({
    selector: 'test-plan-detail',
    templateUrl: 'test-plan-detail.component.html',
    providers: [TestPlanService, AlertService, UserService]
})

export class TestPlanDetailComponent implements OnInit{
    title: string = "Test Plans";
    testPlanForm: FormGroup;
    testPlan: TestPlan;
    testPlanId: string = '';
    customerId: string = '';
    owners: User[];   
        
    constructor(
        private fb: FormBuilder, 
        private testPlanService: TestPlanService,
        private userService: UserService,
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location        
    ){}

    ngOnInit(): void {        
        this.alertService.clear();

        this.route.params.subscribe((params: Params) => {
            this.testPlanId = params['id'];            
        });
                
        this.loadOwners();

        if(this.testPlanId != undefined)
        {            
            this.loadTestPlan();            
        }else{
            this.testPlan = new TestPlan();
            this.testPlanId = undefined;
            this.buildForm();    
            this.startErrors();           
        }    
    }
    
    loadTestPlan(){
        this.testPlanService.getById(this.testPlanId)
            .subscribe(
                response =>{
                    this.testPlan = (response.Data as any);    
                    
                    if(this.testPlan.StartDate != null)
                        this.testPlan.StartDate = new Date(this.testPlan.StartDate);   
                    
                    if(this.testPlan.FinishDate != null)
                        this.testPlan.FinishDate = new Date(this.testPlan.FinishDate); 

                    this.buildForm();           
                },err =>{
                    this.alertService.error(err.message, false);
                });
    }

    buildForm(): void {
        this.testPlanForm = this.fb.group({
            'Name': [
                this.testPlan.Name, [
                    Validators.required
                ]
            ],
            'Description': [
                this.testPlan.Description, [
                    Validators.required
                ]
            ],
            'StartDate': [
                this.testPlan.StartDate
            ],
            'FinishDate': [
                this.testPlan.FinishDate
            ],
            'OwnerId': [
                this.testPlan.OwnerId
            ]
        });

        this.testPlanForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
        
    }

   onValueChanged(data?: any) {
        if (!this.testPlanForm) { return; }
        const form = this.testPlanForm;

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
        this.router.navigate(['/test-plan-detail']);
    }

    update(){
        this.testPlan.TestPlanId = parseInt(this.testPlanId);
        this.testPlanService.update(this.testPlan)
            .subscribe(
                response =>{
                    this.router.navigate(['/test-plan-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });
    }

    insert(){
        this.testPlanService.insert(this.testPlan)
            .subscribe(
                response =>{
                    this.router.navigate(['/test-plan-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });        
    }
    onSubmit() {
        this.testPlan = this.testPlanForm.value;        
        this.testPlan.ProjectId = parseInt(sessionStorage.getItem("selectedProjectId"));
        
        var validationResult = this.testPlanService.validateStartAndFinishDate(this.testPlan);

        if(validationResult.Status == false)
        {            
            this.alertService.error(validationResult.Message);
            return;
        }

        if(this.testPlanId != undefined){
            this.update();
            
        }else{
            this.insert();            
        }
    }

    goBack(){
        this.location.back();
    }

    loadOwners(){
        // Get owners to load select
        this.customerId = sessionStorage.getItem("selectedCustomerId");
        
        this.userService.getByCustomer(this.customerId)
            .subscribe(
                response =>{
                    this.owners = (response.Data as any);        
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    startErrors() {
        this.formErrors.Name = 'Name required',
        this.formErrors.Description = 'Description required'
    }

    formErrors = {
        'Name': '',
        'Description': ''
    };

    validationMessages = {
        'Name': {
            'required': 'Name required.'
        },
        'Description': {
            'required': 'Description required.'
        }
    };
}