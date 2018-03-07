// Imports
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params }  from '@angular/router'
import { Location } from '@angular/common';

/* Datagrid Directive */
import { NgDataGridModel } from '../../_shared/directives/datagrid/ng-datagrid.model';
import { PaginationComponent } from '../../_shared/directives/datagrid/pagination.component';
import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

import { UsersInProjectsService } from './user-project.service';
import { UsersInProjects } from './user-project.model';

import { UserService } from '../user/user.service'
import { User } from '../user/user.model';

import { ProjectService } from '../project/project.service'
import { Project } from '../project/project.model';

@Component({
  selector: 'user-project',
  templateUrl: 'user-project.component.html',
  providers: [ UsersInProjectsService, AlertService, UserService, ProjectService ],
})

export class UsersInProjectsComponent implements OnInit{
    title = 'Add Users to Projects';
    userInProjectForm: FormGroup;
    userInProject: UsersInProjects;
    usersInProjects : UsersInProjects[];    
    customerId: string = '';
    table: NgDataGridModel<UsersInProjects>;      
    projects: Project[];
    users: User[];

    constructor(
        private fb: FormBuilder, 
        private userInProjectService: UsersInProjectsService, 
        private projectService: ProjectService,
        private userService: UserService,
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
    ) {}

    ngOnInit() {    
        this.alertService.clear();

        this.customerId = sessionStorage.getItem("selectedCustomerId");
        this.userInProject = new UsersInProjects();
        
        this.loadProjects();
        this.buildForm();
        this.dataGridLoad();
    }

    loadProjects(){
        this.projectService.getByCustomer(this.customerId)
            .subscribe(
                response =>{
                    this.projects = (response.Data as any);                    
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    changeSelectedProject(selectedValue: string){
        this.loadUsers(selectedValue);
    }

    loadUsers(selectedProjectId: string){
        this.userService.getByNotInProject(selectedProjectId)
            .subscribe(
                response =>{
                    this.users = (response.Data as any);                    
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    buildForm(): void {
        this.userInProjectForm = this.fb.group({
            'UserId': [
                this.userInProject.UserId, [
                    Validators.required
                ]
            ],
            'ProjectId': [
                this.userInProject.ProjectId, [
                    Validators.required
                ]
            ]
        });

        this.userInProjectForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
        
    }

   onValueChanged(data?: any) {
        if (!this.userInProjectForm) { return; }
        const form = this.userInProjectForm;

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

    dataGridLoad(){
        this.table = new NgDataGridModel<UsersInProjects>([]);
        
        this.userInProjectService.getByCustomer(this.customerId)
            .subscribe(
                response =>{                
                    this.usersInProjects = JSON.parse(JSON.stringify(response.Data as any));    
                    for(var user of this.usersInProjects){                             
                        this.table.items.push(user);                   
                    }
                }, err =>{
                    this.alertService.error(err.message);
                });         
    }

    onSubmit() {
        this.alertService.clear();

        this.userInProject = this.userInProjectForm.value;  

        this.userInProjectService.insert(this.userInProject)
            .subscribe(
                respone => {
                    this.loadUsers(this.userInProject.ProjectId.toString());
                    this.dataGridLoad();
                }, err =>{
                    this.alertService.error(err.message);
                });        
    }

    delete(userInProjectId: string){
        this.alertService.clear();

        this.userInProjectService.delete(userInProjectId)
            .subscribe(
                respone => {
                    this.loadUsers(this.userInProject.ProjectId.toString());
                    this.dataGridLoad();
                }, err =>{
                    this.alertService.error(err.message);
                });        
    }

    goBack(){
        this.location.back();
    }

    formErrors = {
        'UserId': '',
        'ProjectId': ''
    };

    validationMessages = {
        'UserId': {
            'required': 'User required.'
        },
        'ProjectId': {
            'required': 'Project required.'
        }
    };
}