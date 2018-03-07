import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { TestSuiteService } from './test-suite.service';
import { TestSuite } from './test-suite.model';

describe('Service: TestSuite', () => {
    let testSuiteService: TestSuiteService;
    let mockBackend: MockBackend; 

    let testSuitesToQuery: TestSuite[] = [
        { TestSuiteId: 1, Name: "TestPlan 1 - Test Suite 1", Description: "TestPlan 1 - Test Suite 1",HasChildren: true, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 2, Name: "TestPlan 1 - Test Suite 2", Description: "TestPlan 1 - Test Suite 2",HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: 1, ParentTestSuite: "TestPlan 1 - Test Suite 1", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 3, Name: "TestPlan 1 - Test Suite 3", Description: "TestPlan 1 - Test Suite 3",HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 4, Name: "TestPlan 1 - Test Suite 4", Description: "TestPlan 1 - Test Suite 4",HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: 1, ParentTestSuite: "TestPlan 1 - Test Suite 1", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 5, Name: "TestPlan 1 - Test Suite 5", Description: "TestPlan 1 - Test Suite 5",HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 6, Name: "TestPlan 1 - Test Suite 6", Description: "TestPlan 1 - Test Suite 6",HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 7, Name: "TestPlan 2 - Test Suite 1", Description: "TestPlan 2 - Test Suite 1",HasChildren: false, TestPlanId: 2, TestPlan: "Test Plan 2",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 8, Name: "TestPlan 2 - Test Suite 2", Description: "TestPlan 2 - Test Suite 2",HasChildren: false, TestPlanId: 2, TestPlan: "Test Plan 2",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 9, Name: "TestPlan 2 - Test Suite 3", Description: "TestPlan 2 - Test Suite 3",HasChildren: false, TestPlanId: 2, TestPlan: "Test Plan 2",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
        { TestSuiteId: 10, Name: "TestPlan 2 - Test Suite 4", Description: "TestPlan 2 - Test Suite 4",HasChildren: false, TestPlanId: 2, TestPlan: "Test Plan 2",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() },
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                TestSuiteService,
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

        testSuiteService = getTestBed().get(TestSuiteService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get test suite by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let testSuite = testSuitesToQuery.find(c => c.TestSuiteId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(testSuite)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testSuiteService.getById("1").subscribe(response =>{   
            let testSuite = JSON.parse(JSON.stringify(response.Data as any));
            expect(testSuite.TestSuiteId).toEqual(1);
            expect(testSuite.Name).toEqual("TestPlan 1 - Test Suite 1");

        });         
    }));

    it('Should not get test suite by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Test Suite not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        testSuiteService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("Test Suite not Found");
            let testSuite = JSON.parse(JSON.stringify(response.Data as any));
            expect(testSuite).toBeNull();            
        });         
    }));

    it('Return error 500 on get test suite by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testSuite: TestSuite[] = []
        let errorInfo: string;
        testSuiteService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new test suite', async(() =>{        
        let testSuite: TestSuite;
        testSuite = {TestSuiteId: 99,  Name: "TestPlan 1 - Test Suite 1", Description: "TestPlan 1 - Test Suite 1", HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        testSuiteService.insert(testSuite).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let testSuite: TestSuite = {TestSuiteId: 99,  Name: "TestPlan 1 - Test Suite 1", Description: "TestPlan 1 - Test Suite 1", HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testSuites: TestSuite[] = []
        let errorInfo: string;
        testSuiteService.insert(testSuite).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update test suite', async(() =>{        
        let testSuite: TestSuite;
        testSuite = {TestSuiteId: 99, Name: "TestPlan 1 - Test Suite 1", Description: "TestPlan 1 - Test Suite 1", HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testSuiteService.update(testSuite).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let testSuite: TestSuite = {TestSuiteId: 99,  Name: "TestPlan 1 - Test Suite 1", Description: "TestPlan 1 - Test Suite 1", HasChildren: false, TestPlanId: 1, TestPlan: "Test Plan 1",ParentTestSuiteId: null, ParentTestSuite: "", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date() }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testSuites: TestSuite[] = []
        let errorInfo: string;
        testSuiteService.update(testSuite).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should delete test suite', async(() =>{        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testSuiteService.delete("1").subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testSuites: TestSuite[] = []
        let errorInfo: string;
        testSuiteService.delete("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
});