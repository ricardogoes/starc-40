import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { TestPlanService } from './test-plan.service';
import { TestPlan } from './test-plan.model';

//import {} from 'angular-moment';

describe('Service: TestPlan', () => {
    let testPlanService: TestPlanService;
    let mockBackend: MockBackend;    

    let testPlansToQuery: TestPlan[] = [
        { TestPlanId: 1, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 2, Name: "TestPlan 2", Description: "TestPlan 2", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 3, Name: "TestPlan 3", Description: "TestPlan 3", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 4, Name: "TestPlan 4", Description: "TestPlan 4", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 5, Name: "TestPlan 5", Description: "TestPlan 5", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 6, Name: "TestPlan 6", Description: "TestPlan 6", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 7, Name: "TestPlan 7", Description: "TestPlan 7", ProjectId: 2, Project: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 8, Name: "TestPlan 8", Description: "TestPlan 8", ProjectId: 2, Project: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 9, Name: "TestPlan 9", Description: "TestPlan 9", ProjectId: 2, Project: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
        { TestPlanId: 10, Name: "TestPlan 10", Description: "TestPlan 10", ProjectId: 2, Project: "Argo", OwnerId: 3, Owner: "Admin Argo", StartDate: null, FinishDate: null, Status: false, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" },
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                TestPlanService,
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

        testPlanService = getTestBed().get(TestPlanService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get test plans by project', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let activeTestPlans = testPlansToQuery.filter(c => c.ProjectId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(activeTestPlans)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.getByProject("1").subscribe(response =>{   
            let testPlans = JSON.parse(JSON.stringify(response.Data as any));
            expect(testPlans.length).toEqual(6);
        });         
    }));

    it('Should return error 500 on get test plans by project', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.getByProject("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get all active testPlans', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let activeTestPlans = testPlansToQuery.filter(c => c.Status == true && c.ProjectId == 1);
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(activeTestPlans)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.getActiveByProject("1").subscribe(response =>{   
            let testPlans = JSON.parse(JSON.stringify(response.Data as any));
            expect(testPlans.length).toEqual(4);
        });         
    }));

    it('Return error 500 on get all active testPlans', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.getActiveByProject("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get testPlan by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let testPlan = testPlansToQuery.find(c => c.TestPlanId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(testPlan)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.getById("1").subscribe(response =>{   
            let testPlan = JSON.parse(JSON.stringify(response.Data as any));
            expect(testPlan.TestPlanId).toEqual(1);
            expect(testPlan.Name).toEqual("TestPlan 1");

        });         
    }));

    it('Should not get testPlan by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"TestPlan not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("TestPlan not Found");
            let testPlan = JSON.parse(JSON.stringify(response.Data as any));
            expect(testPlan).toBeNull();            
        });         
    }));

    it('Return error 500 on get testPlan by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new testPlan', async(() =>{        
        let testPlan: TestPlan;
        testPlan = {TestPlanId: 99, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.insert(testPlan).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let testPlan: TestPlan = {TestPlanId: 99, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.insert(testPlan).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update testPlan', async(() =>{        
        let testPlan: TestPlan;
        testPlan = {TestPlanId: 99, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.update(testPlan).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let testPlan: TestPlan = {TestPlanId: 99, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: null, FinishDate: null, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.update(testPlan).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should change status', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let testPlan = testPlansToQuery.find(c => c.TestPlanId.toString() == "1");
            
            let options = new ResponseOptions({
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.changeStatus(1).subscribe(response =>{   
            expect(response.status).toEqual(204);    

        });         
    }));

    it('Should not changestatus by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"TestPlan not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        testPlanService.changeStatus(1).subscribe(
            response =>response,
            error =>{
                expect(error.status).toEqual(404);
                expect(error.Message).toEqual("TestPlan not Found");
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

        let testPlans: TestPlan[] = []
        let errorInfo: string;
        testPlanService.changeStatus(1).subscribe(
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
        
        let testPlan: TestPlan;        
        testPlan = { TestPlanId: 1, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: startDate, FinishDate: finishDate, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" };
        
        var validation = testPlanService.validateStartAndFinishDate(testPlan);

        expect(validation.Status).toBeFalsy();
        expect(validation.Message).toEqual("Finish Date must be greater than Start Date");        

        startDate = new Date();
        finishDate = new Date("2017-10-12T00:00:00");

        testPlan = { TestPlanId: 1, Name: "TestPlan 1", Description: "TestPlan 1", ProjectId: 1, Project: "Grupo HDI", OwnerId: 2, Owner: "Admin", StartDate: startDate, FinishDate: finishDate, Status: true, LastUpdatedBy: 1, LastUpdatedDate: new Date, LastUpdatedName: "Administrador Sistema", CreatedBy: 1, CreatedDate: new Date, CreatedName: "Administrador Sistema" };

        validation = testPlanService.validateStartAndFinishDate(testPlan);

        expect(validation.Status).toBeTruthy();        
    }));
});