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

import { User } from './user.model';
import { UserService } from './user.service';

@Component({
  selector: 'user-list',
  templateUrl: 'user-list.component.html',
  providers: [ UserService, AlertService ],
})

export class UserListComponent implements OnInit{
    title = 'Users';
    users : User[];    
    customerId: string = '';
    table: NgDataGridModel<User>;      

    constructor(
        private userService: UserService, 
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
        this.table = new NgDataGridModel<User>([]);
        
        this.userService.getByCustomer(this.customerId)
            .subscribe(
                response =>{                
                    this.users = JSON.parse(JSON.stringify(response.Data as any));    
                    for(var user of this.users){                             
                        this.table.items.push(user);                   
                    }
                }, err =>{
                    this.alertService.error(err.message);
                });         
    }

    edit(user: User){        
        this.router.navigate(['/user-detail', user.UserId]);
    }

    changeStatus(userId: number){
        this.userService.changeStatus(userId)
            .subscribe(
                response => {
                    this.dataGridLoad()
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    addNew(){
        this.router.navigate(['/user-detail']);
    } 

    goBack(){
        this.location.back();
    }
}