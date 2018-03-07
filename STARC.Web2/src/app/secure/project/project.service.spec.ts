import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ProjectService } from './project.service';
import { Project } from './project.model';

//import {} from 'angular-moment';

describe('Service: Project', () => {
    let projectService: ProjectService;
    let mockBackend: MockBackend;    

    let projectsToQuery: Project[] = [
        { ProjectId: 1, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 2, Name: "Project 2", Description: "Project 2", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 3, Name: "Project 3", Description: "Project 3", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 4, Name: "Project 4", Description: "Project 4", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 5, Name: "Project 5", Description: "Project 5", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 6, Name: "Project 6", Description: "Project 6", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 7, Name: "Project 7", Description: "Project 7", CustomerId: 2, Customer: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 8, Name: "Project 8", Description: "Project 8", CustomerId: 2, Customer: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 9, Name: "Project 9", Description: "Project 9", CustomerId: 2, Customer: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { ProjectId: 10, Name: "Project 10", Description: "Project 10", CustomerId: 2, Customer: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                ProjectService,
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

        projectService = getTestBed().get(ProjectService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get all active projects', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let activeProjects = projectsToQuery.filter(c => c.Status == true && c.CustomerId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(activeProjects)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        projectService.getActiveByCustomer("1").subscribe(response =>{   
            let projects = JSON.parse(JSON.stringify(response.Data as any));
            expect(projects.length).toEqual(4);
        });         
    }));

    it('Return error 500 on get all active projects', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let projects: Project[] = []
        let errorInfo: string;
        projectService.getActiveByCustomer("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get project by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let project = projectsToQuery.find(c => c.ProjectId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(project)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        projectService.getById("1").subscribe(response =>{   
            let project = JSON.parse(JSON.stringify(response.Data as any));
            expect(project.ProjectId).toEqual(1);
            expect(project.Name).toEqual("Project 1");

        });         
    }));

    it('Should not get project by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Project not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        projectService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("Project not Found");
            let project = JSON.parse(JSON.stringify(response.Data as any));
            expect(project).toBeNull();            
        });         
    }));

    it('Return error 500 on get project by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let projects: Project[] = []
        let errorInfo: string;
        projectService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new project', async(() =>{        
        let project: Project;
        project = {ProjectId: 99, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        projectService.insert(project).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let project: Project = {ProjectId: 99, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let projects: Project[] = []
        let errorInfo: string;
        projectService.insert(project).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update project', async(() =>{        
        let project: Project;
        project = {ProjectId: 99, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        projectService.update(project).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let project: Project = {ProjectId: 99, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let projects: Project[] = []
        let errorInfo: string;
        projectService.update(project).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should change status', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let project = projectsToQuery.find(c => c.ProjectId.toString() == "1");
            
            let options = new ResponseOptions({
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        projectService.changeStatus(1).subscribe(response =>{   
            expect(response.status).toEqual(204);    

        });         
    }));

    it('Should not changestatus by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Project not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        projectService.changeStatus(1).subscribe(
            response =>response,
            error =>{
                expect(error.status).toEqual(404);
                expect(error.Message).toEqual("Project not Found");
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

        let projects: Project[] = []
        let errorInfo: string;
        projectService.changeStatus(1).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get projects by customer', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let projects = projectsToQuery.filter(c => c.CustomerId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(projects)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        projectService.getByCustomer("1").subscribe(response =>{   
            let projects = JSON.parse(JSON.stringify(response.Data as any));
            expect(projects.length).toEqual(6);
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

        let projects: Project[] = []
        let errorInfo: string;
        projectService.getByCustomer("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should validate StartDate cant be greater than FinishDate', async(() =>{        
        let startDate = new Date("2017-10-12T00:00:00");
        let finishDate = new Date();        
        
        let project: Project;        
        project = { ProjectId: 1, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: startDate, FinishDate: finishDate, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" };
        
        var validation = projectService.validateStartAndFinishDate(project);

        expect(validation.Status).toBeFalsy();
        expect(validation.Message).toEqual("Finish Date must be greater than Start Date");        

        startDate = new Date();
        finishDate = new Date("2017-10-12T00:00:00");

        project = { ProjectId: 1, Name: "Project 1", Description: "Project 1", CustomerId: 1, Customer: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: startDate, FinishDate: finishDate, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" };

        validation = projectService.validateStartAndFinishDate(project);

        expect(validation.Status).toBeTruthy();        
    }));
});