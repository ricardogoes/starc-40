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

import { TestPlanService } from './test-plan.service';
import { TestPlan } from './test-plan.model';

@Component({
    selector: 'test-plan-list',
    templateUrl: 'test-plan-list.component.html',
    providers: [TestPlanService, AlertService]
})

export class TestPlanListComponent implements OnInit{
    title: string = "Test Plan";
    testPlans : TestPlan[];    
    projectId: string = '';
    table: NgDataGridModel<TestPlan>;      

    constructor(
        private testPlanService: TestPlanService, 
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        ) {}

    ngOnInit() {    
        this.alertService.clear();

        this.projectId = sessionStorage.getItem("selectedProjectId");
        this.dataGridLoad();
    }
    
    dataGridLoad(){
        this.table = new NgDataGridModel<TestPlan>([]);
        
        this.testPlanService.getByProject(this.projectId)
            .subscribe(response =>{                
                this.testPlans = JSON.parse(JSON.stringify(response.Data as any));  

                for(var testPlan of this.testPlans){                             
                    this.table.items.push(testPlan);                   
                }
            },err =>{
                this.alertService.error(err.message);
            })
    }

    edit(testPlan: TestPlan){
        this.router.navigate(['/test-plan-detail', testPlan.TestPlanId]);
    }

    changeStatus(testPlanId: number){
        this.testPlanService.changeStatus(testPlanId)
            .subscribe(
                response => {
                    this.dataGridLoad()
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    openTestPlan(testPlan: TestPlan){
        this.router.navigate(['/test-plan-manager']);
    }

    addNew(){
        this.router.navigate(['/test-plan-detail']);
    } 

    goBack(){
        this.location.back();
    }
}