// Imports
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }  from '@angular/router'
import { Location } from '@angular/common';

/* Datagrid Directive */
import { NgDataGridModel } from '../../_shared/directives/datagrid/ng-datagrid.model';
import { PaginationComponent } from '../../_shared/directives/datagrid/pagination.component';
import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

/* Filters */
import { StatusFilter } from '../../_shared/filters/status.filter'

import { Project } from './project.model';
import { ProjectService } from './project.service';

@Component({
  selector: 'project-list',
  templateUrl: 'project-list.component.html',
  providers: [ProjectService, AlertService],
})

export class ProjectListComponent implements OnInit{
    title = 'Projects';
    projects : Project[];    
    customerId: string = '';
    table: NgDataGridModel<Project>;      

    constructor(
        private projectService: ProjectService, 
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        ) {}

    ngOnInit() {    
        this.alertService.clear();

        this.customerId = sessionStorage.getItem("selectedCustomerId");
        this.dataGridLoad();
    }

    dataGridLoad(){
        this.table = new NgDataGridModel<Project>([]);
        
        this.projectService.getByCustomer(this.customerId)
            .subscribe(response =>{                
                this.projects = JSON.parse(JSON.stringify(response.Data as any));  

                for(var project of this.projects){                             
                    this.table.items.push(project);                   
                }
            },err =>{
                this.alertService.error(err.message);
            })
    }

    edit(project: Project){
        this.router.navigate(['/project-detail', project.ProjectId]);
    }

    changeStatus(projectId: number){
        this.projectService.changeStatus(projectId)
            .subscribe(
                response => {
                    this.dataGridLoad()
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    associateUsersTo(project: Project){
        this.router.navigate(['/user-project']);
    }

    addNew(){
        this.router.navigate(['/project-detail']);
    } 

    goBack(){
        this.location.back();
    }
}