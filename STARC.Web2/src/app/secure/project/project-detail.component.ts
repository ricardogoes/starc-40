/* tslint:disable: member-ordering forin */
import { Component, OnInit }  from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

import { ProjectService } from './project.service';
import { Project } from './project.model';

import { UserService } from '../user/user.service';
import { User } from '../user/user.model';

@Component({
    selector: 'project-detail',
    templateUrl: 'project-detail.component.html',
    providers: [ProjectService, UserService, AlertService]
})

export class ProjectDetailComponent implements OnInit{
    title: string = "Projects";
    projectForm: FormGroup;
    project: Project;
    projectId: string = '';
    customerId: string = '';
    owners: User[];   
        
    constructor(
        private fb: FormBuilder, 
        private projectService: ProjectService,
        private userService: UserService,
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location        
    ){}

    ngOnInit(): void {        
        this.alertService.clear();

        this.route.params.subscribe((params: Params) => {
            this.projectId = params['id'];            
        });
                
        this.loadOwners();

        if(this.projectId != undefined)
        {            
            this.loadProject();            
        }else{
            this.project = new Project();
            this.projectId = undefined;
            this.buildForm();    
            this.startErrors();           
        }    
    }
    
    loadProject(){
        this.projectService.getById(this.projectId)
            .subscribe(
                response =>{
                    this.project = (response.Data as any);    
                    
                    if(this.project.StartDate != null)
                        this.project.StartDate = new Date(this.project.StartDate);   
                    
                    if(this.project.FinishDate != null)
                        this.project.FinishDate = new Date(this.project.FinishDate); 

                    this.buildForm();           
                },err =>{
                    this.alertService.error(err.message, false);
                });
    }

    buildForm(): void {
        this.projectForm = this.fb.group({
            'Name': [
                this.project.Name, [
                    Validators.required
                ]
            ],
            'Description': [
                this.project.Description, [
                    Validators.required
                ]
            ],
            'StartDate': [
                this.project.StartDate
            ],
            'FinishDate': [
                this.project.FinishDate
            ],
            'OwnerId': [
                this.project.OwnerId
            ]
        });

        this.projectForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
        
    }

   onValueChanged(data?: any) {
        if (!this.projectForm) { return; }
        const form = this.projectForm;

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
        this.router.navigate(['/project-detail']);
    }

    update(){
        this.project.ProjectId = parseInt(this.projectId);
        this.projectService.update(this.project)
            .subscribe(
                response =>{
                    this.router.navigate(['/project-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });
    }

    insert(){
        this.projectService.insert(this.project)
            .subscribe(
                response =>{
                    this.router.navigate(['/project-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });        
    }
    onSubmit() {
        this.project = this.projectForm.value;        
        this.project.CustomerId = parseInt(sessionStorage.getItem("selectedCustomerId"));
        
        var validationResult = this.projectService.validateStartAndFinishDate(this.project);

        if(validationResult.Status == false)
        {            
            this.alertService.error(validationResult.Message);
            return;
        }

        if(this.projectId != undefined){
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
