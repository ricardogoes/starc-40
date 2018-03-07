// Imports
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { Location } from '@angular/common';

/* Datagrid Directive */
import { PaginationComponent } from '../../_shared/directives/datagrid/pagination.component';
import { NgDataGridModel } from '../../_shared/directives/datagrid/ng-datagrid.model';
import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

/* Filters */
import { StatusFilter } from '../../_shared/filters/status.filter'
import { CpfCnpjFilter } from '../../_shared/filters/cpf-cnpj.filter'

/* Custom Features */
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Component({
  selector: 'customer-list',
  templateUrl: 'customer-list.component.html',
  providers: [CustomerService, AlertService]
})

export class CustomerListComponent implements OnInit{
    title = 'Customers';
    customers : Customer[];    
    table: NgDataGridModel<Customer>;       

    constructor(
        private customerService: CustomerService, 
        private alertService: AlertService,
        private router: Router, 
        private location: Location
        ) {}

    ngOnInit() {            
        this.alertService.clear();
         this.customers = [];
         this.dataGridLoad();
    }

    dataGridLoad(){
        this.table = new NgDataGridModel<Customer>([]);
        
        this.customerService.getAll()
            .subscribe(
                response =>{                
                    this.customers = JSON.parse(JSON.stringify(response.Data as any));    
                    
                    for(var customer of this.customers){                             
                        this.table.items.push(customer);                   
                    }                    
                },err =>{
                    this.alertService.error(err.message);
                });           
    }

    edit(customer: Customer){
        this.router.navigate(['/customer-detail', customer.CustomerId]);
    }

    changeStatus(customerId: number){
        this.customerService.changeStatus(customerId)
            .subscribe(
                response => {
                    this.dataGridLoad()
                }, err =>{
                    this.alertService.error(err.message);
                });
    }

    openProjectsFrom(customer: Customer){
        sessionStorage.setItem("selectedCustomerId", customer.CustomerId.toString());       
        this.router.navigate(['/project-list']);        
    }

    openUsersFrom(customer: Customer){
        sessionStorage.setItem("selectedCustomerId", customer.CustomerId.toString());       
        this.router.navigate(['/user-list']); 
    }

    addNew(){
        this.router.navigate(['/customer-detail']);
    } 

    goBack(){
        this.location.back();
    }
}