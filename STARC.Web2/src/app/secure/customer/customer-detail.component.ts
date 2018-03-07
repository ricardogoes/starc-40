/* tslint:disable: member-ordering forin */
import { Component, OnInit }  from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AlertService } from '../../_shared/directives/alert-notification/alert.service';
import { CpfCnpjValidate } from '../../_shared/directives/cpf-cnpj-validator/cpf-cnpj.directive';

import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Component({
    selector: 'customer-detail',
    templateUrl: 'customer-detail.component.html',
    providers: [ CustomerService, AlertService ]
})

export class CustomerDetailComponent implements OnInit{    
    title: string = "Customers";
    customerForm: FormGroup;
    customer: Customer;
    customerId: string = '';
    txtDocumentId: string;

    constructor(
        private fb: FormBuilder, 
        private customerService: CustomerService,
        private alertService: AlertService,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
    ){}

    ngOnInit(): void {        
        this.alertService.clear();

        //Get parameter from route
        this.route.params.subscribe((params: Params) => {
            this.customerId = params['id'];            
        });
        
        if(this.customerId != undefined)
        {            
            this.loadCustomer();
        }else{
            this.customer = new Customer();
            this.customerId = undefined;
            this.txtDocumentId = "";
            this.buildForm();
            this.startErrors();
        }       
    }

    loadCustomer(){
        this.customerService.getById(this.customerId).
            subscribe(
                response =>{
                    this.customer = (response.Data as any);
                    this.txtDocumentId = this.customer.DocumentId;
                    this.buildForm();                    
                },err =>{
                    this.alertService.error(err.message, false);
                });
    }
    
    buildForm(): void {
        this.customerForm = this.fb.group({
            'Name': [
                this.customer.Name, [
                    Validators.required                    
                ]
            ],
            'DocumentId': [
                this.customer.DocumentId, [
                    Validators.required,  
                    CpfCnpjValidate
                ]
            ],
            'Address': [
                this.customer.Address, [
                    Validators.required
                ]
            ]
        });

        this.customerForm.valueChanges.subscribe(data => this.onValueChanged(data));
        this.onValueChanged(); // (re)set validation messages now
    }

   onValueChanged(data?: any) {
        if (!this.customerForm) { return; }
        const form = this.customerForm;

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
        this.router.navigate(['/customer-detail']);
    }

    update(){
        this.customer.CustomerId = parseInt(this.customerId);

        this.customerService.update(this.customer)
            .subscribe(
                response =>{
                    this.router.navigate(['/customer-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });
    }

    insert(){
        this.customerService.insert(this.customer)
            .subscribe(
                response =>{
                    this.router.navigate(['/customer-list']);
                }, err =>{
                    this.alertService.error(err.message, false);
                });        
    }
            
    onSubmit() {
        this.alertService.clear();
        
        this.customer = this.customerForm.value;
        
        if(this.customerId != undefined){
            this.update();
        }else{
            this.insert();
        }        
    }

    replaceDocumentId(value: string){
        this.txtDocumentId = value.replace(".","").replace("-","").replace("/","");
    }

    goBack(){
        this.location.back();
    }

    startErrors() {
        this.formErrors.Name = 'Name required',
        this.formErrors.DocumentId = 'DocumentId required',
        this.formErrors.Address = 'Address required'
    }

    formErrors = {
        'Name': '',
        'DocumentId': '',
        'Address': '',
    };

    validationMessages = {
        'Name': {
            'required': 'Name is required.'
        },
        'DocumentId': {
            'required': 'DocumentId is required.',
            'cpfCnpj': 'DocumentId is invalid'
        },
        'Address': {
            'required': 'Address is required.'
        }
    };
}
