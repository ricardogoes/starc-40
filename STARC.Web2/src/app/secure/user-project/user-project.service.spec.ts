import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { UsersInProjectsService } from './user-project.service';
import { UsersInProjects } from './user-project.model';

//import {} from 'angular-moment';

describe('Service: UsersInProjects', () => {
    let userService: UsersInProjectsService;
    let mockBackend: MockBackend;    

    let usersToQuery: UsersInProjects[] = [
        { UserInProjectId: 1, UserId: 2, User: "ricardo.goes", ProjectId: 1, Project: "Projeto 1", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        { UserInProjectId: 2, UserId: 2, User: "ricardo.goes", ProjectId: 2, Project: "Projeto 2", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        { UserInProjectId: 3, UserId: 3, User: "lucilene.goes", ProjectId: 1, Project: "Projeto 1", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        { UserInProjectId: 4, UserId: 3, User: "lucilene.goes", ProjectId: 2, Project: "Projeto 2", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        { UserInProjectId: 5, UserId: 4, User: "caroline.goes", ProjectId: 1, Project: "Projeto 1", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        { UserInProjectId: 6, UserId: 5, User: "manoel.goes", ProjectId: 1, Project: "Projeto 1", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}, 
        
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                UsersInProjectsService,
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

        userService = getTestBed().get(UsersInProjectsService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get user in projct by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let user = usersToQuery.find(c => c.UserInProjectId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(user)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getById("1").subscribe(response =>{   
            let user = JSON.parse(JSON.stringify(response.Data as any));
            expect(user.UserInProjectId).toEqual(1);
            expect(user.User).toEqual("ricardo.goes");
            expect(user.Project).toEqual("Projeto 1");

        });         
    }));

    it('Should not get user in project by invalid id', async(() =>{        
    
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

    it('Return error 500 on get user in project by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });
        
        let errorInfo: string;
        userService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new user in project', async(() =>{        
        let user: UsersInProjects;
        user = { UserInProjectId: 99, UserId: 2, User: "ricardo.goes", ProjectId: 3, Project: "Projeto 3", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}
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
        let user: UsersInProjects = { UserInProjectId: 99, UserId: 2, User: "ricardo.goes", ProjectId: 3, Project: "Projeto 3", CreatedBy:1, CreatedName: "Administrador Sistema", CreatedDate: new Date()}

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let errorInfo: string;
        userService.insert(user).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should delete user in project', async(() =>{        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        userService.delete("1").subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on delete', async(() =>{        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let errorInfo: string;
        userService.delete("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get users by customer', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(usersToQuery)+'}',
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

        let errorInfo: string;
        userService.getByCustomer("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

   it('Should get users by project', async(() =>{            
        let usersInProjects = usersToQuery.filter(c => c.ProjectId == 1);
        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(usersInProjects)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getByProject("1").subscribe(response =>{   
            let users = JSON.parse(JSON.stringify(response.Data as any));
            expect(users.length).toEqual(4);
        });         
    }));

    it('Return error 500 on get by project', async(() =>{                

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let errorInfo: string;
        userService.getByProject("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get users by user', async(() =>{            
        let usersInProjects = usersToQuery.filter(c => c.UserId == 2);
        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(usersInProjects)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userService.getByUser("2").subscribe(response =>{   
            let users = JSON.parse(JSON.stringify(response.Data as any));
            expect(users.length).toEqual(2);
        });         
    }));

    it('Return error 500 on get by user', async(() =>{                

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let errorInfo: string;
        userService.getByUser("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
});