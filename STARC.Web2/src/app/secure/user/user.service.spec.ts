import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { UserService } from './user.service';
import { User } from './user.model';

//import {} from 'angular-moment';

describe('Service: User', () => {
    let userService: UserService;
    let mockBackend: MockBackend;    

    let usersToQuery: User[] = [
        { UserId: 1, FirstName: "User", LastName: "1", Username: "user1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer:"Grupo HDI", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 2, FirstName: "User", LastName: "2", Username: "user2", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer:"Grupo HDI",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 3, FirstName: "Administrador", LastName: "1", Username: "admin1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"Administrador", UserProfileId: 2, Status: true, CustomerId:1, Customer:"Grupo HDI",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 4, FirstName: "User", LastName: "3", Username: "user3", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer:"Grupo HDI",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 5, FirstName: "User", LastName: "4", Username: "user4", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: false, CustomerId:1, Customer:"Grupo HDI",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 6, FirstName: "User", LastName: "5", Username: "user5", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: false, CustomerId:1, Customer:"Grupo HDI",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 7, FirstName: "Administrador", LastName: "2", Username: "admin2", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"Administrador", UserProfileId: 2, Status: true, CustomerId:2, Customer:"Argo Solutions",LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 8, FirstName: "User", LastName: "6", Username: "user6", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:2, Customer:"Argo Solutions", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 9, FirstName: "User", LastName: "7", Username: "user7", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: false, CustomerId:2, Customer:"Argo Solutions", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"},
        { UserId: 10,FirstName: "User", LastName: "8", Username: "user8", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:2, Customer:"Argo Solutions", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                UserService,
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

        userService = getTestBed().get(UserService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get user by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let user = usersToQuery.find(c => c.UserId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(user)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getById("1").subscribe(response =>{   
            let user = JSON.parse(JSON.stringify(response.Data as any));
            expect(user.UserId).toEqual(1);
            expect(user.FirstName).toEqual("User");
            expect(user.LastName).toEqual("1");

        });         
    }));

    it('Should not get user by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"User not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        userService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("User not Found");
            let user = JSON.parse(JSON.stringify(response.Data as any));
            expect(user).toBeNull();            
        });         
    }));

    it('Return error 500 on get user by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let users: User[] = []
        let errorInfo: string;
        userService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new user', async(() =>{        
        let user: User;
        user = {UserId: 99, FirstName: "User", LastName: "1", Username: "user1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer: "Grupo HDI", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        userService.insert(user).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let user: User = {UserId: 99, FirstName: "User", LastName: "1", Username: "user1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer: "Grupo HDI", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let users: User[] = []
        let errorInfo: string;
        userService.insert(user).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update user', async(() =>{        
        let user: User;
        user = {UserId: 99, FirstName: "User", LastName: "1", Username: "user1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer: "Grupo HDI", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        userService.update(user).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let user: User = {UserId: 99, FirstName: "User", LastName: "1", Username: "user1", Email: "user1@email.com", Password:"password", ConfirmPassword:"password", ProfileName:"usuario", UserProfileId: 3, Status: true, CustomerId:1, Customer: "Grupo HDI", LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema"}

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let users: User[] = []
        let errorInfo: string;
        userService.update(user).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should change status', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let user = usersToQuery.find(c => c.UserId.toString() == "1");
            
            let options = new ResponseOptions({
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        userService.changeStatus(1).subscribe(response =>{   
            expect(response.status).toEqual(204);    

        });         
    }));

    it('Should not changestatus by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"User not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        userService.changeStatus(1).subscribe(
            response =>response,
            error =>{
                expect(error.status).toEqual(404);
                expect(error.Message).toEqual("User not Found");
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

        let users: User[] = []
        let errorInfo: string;
        userService.changeStatus(1).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get users by customer', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let users = usersToQuery.filter(c => c.CustomerId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(users)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getByCustomer("1").subscribe(response =>{   
            let users = JSON.parse(JSON.stringify(response.Data as any));
            expect(users.length).toEqual(6);
        });         
    }));

    it('Return error 500 on get by customer', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let users: User[] = []
        let errorInfo: string;
        userService.getByCustomer("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get users not in project', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let users = usersToQuery.filter(c => c.CustomerId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(users)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getByNotInProject("1").subscribe(response =>{   
            let users = JSON.parse(JSON.stringify(response.Data as any));
            expect(users.length).toEqual(6);
        });         
    }));

    it('Return error 500 on get by customer', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let users: User[] = []
        let errorInfo: string;
        userService.getByNotInProject("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
});