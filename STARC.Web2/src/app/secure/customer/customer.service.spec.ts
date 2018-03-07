import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { CustomerService } from './customer.service';
import { Customer } from './customer.model';

describe('Service: Customer', () => {
    let customerService: CustomerService;
    let mockBackend: MockBackend;    

    let customersToQuery: Customer[] = [
        { CustomerId: 1, Name: "Customer 1", Address: "Address 1", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 2, Name: "Customer 2", Address: "Address 2", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 3, Name: "Customer 3", Address: "Address 3", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 4, Name: "Customer 4", Address: "Address 4", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 5, Name: "Customer 5", Address: "Address 5", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 6, Name: "Customer 6", Address: "Address 6", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 7, Name: "Customer 7", Address: "Address 7", DocumentId: "34538886858", Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 8, Name: "Customer 8", Address: "Address 8", DocumentId: "34538886858", Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 9, Name: "Customer 9", Address: "Address 9", DocumentId: "34538886858", Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { CustomerId: 10, Name: "Customer 10", Address: "Address 10", DocumentId: "34538886858", Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                CustomerService,
                MockBackend,
                { 
                    deps: [
                        MockBackend,
                        BaseRequestOptions 
                    ],
                    provide: Http,
                    useFactory: (backend: MockBackend, defaultOptions: BaseRequestOptions) => {
                        return new Http(backend, defaultOptions);
                    }
                 }
            ]
        });

        customerService = getTestBed().get(CustomerService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get all customers', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(customersToQuery)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        customerService.getAll().subscribe(response =>{   
            let customers = JSON.parse(JSON.stringify(response.Data as any));
            expect(customers.length).toEqual(10);
            
            var i = 0;
            for(var customer of customers){                             
                i++;
                expect(customer.CustomerId).toEqual(i);
            }   
        });         
    }));

    it('Return error 500 on get all customers', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.getAll().subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get all active customers', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let activeCustomers = customersToQuery.filter(c => c.Status == true);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(activeCustomers)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        customerService.getActive().subscribe(response =>{   
            let customers = JSON.parse(JSON.stringify(response.Data as any));
            expect(customers.length).toEqual(7);
        });         
    }));

    it('Return error 500 on get all active customers', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.getActive().subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get customer by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let customer = customersToQuery.find(c => c.CustomerId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(customer)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        customerService.getById("1").subscribe(response =>{   
            let customer = JSON.parse(JSON.stringify(response.Data as any));
            expect(customer.CustomerId).toEqual(1);
            expect(customer.Name).toEqual("Customer 1");

        });         
    }));

    it('Should not get customer by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Customer not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        customerService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("Customer not Found");
            let customer = JSON.parse(JSON.stringify(response.Data as any));
            expect(customer).toBeNull();            
        });         
    }));

    it('Return error 500 on get customer by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new customer', async(() =>{        
        let customer: Customer;
        customer = {CustomerId: 99, Name: "Customer UnitTest", Address: "Av. Teste, 123", DocumentId: "34538886858",Status:true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        customerService.insert(customer).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let customer: Customer = {CustomerId: 99, Name: "Customer UnitTest", Address: "Av. Teste, 123", DocumentId: "34538886858",Status:true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.insert(customer).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update customer', async(() =>{        
        let customer: Customer;
        customer = {CustomerId: 99, Name: "Customer UnitTest", Address: "Av. Teste, 123", DocumentId: "34538886858",Status:true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        customerService.update(customer).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let customer: Customer = {CustomerId: 99, Name: "Customer UnitTest", Address: "Av. Teste, 123", DocumentId: "34538886858",Status:true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.update(customer).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should change status', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let customer = customersToQuery.find(c => c.CustomerId.toString() == "1");
            
            let options = new ResponseOptions({
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        customerService.changeStatus(1).subscribe(response =>{   
            expect(response.status).toEqual(204);    

        });         
    }));

    it('Should not changestatus by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Customer not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        customerService.changeStatus(1).subscribe(
            response =>response,
            error =>{
                expect(error.status).toEqual(404);
                expect(error.Message).toEqual("Customer not Found");
            });         
    }));

    it('Return error 500 on changeStatus', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let customers: Customer[] = []
        let errorInfo: string;
        customerService.changeStatus(1).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
});