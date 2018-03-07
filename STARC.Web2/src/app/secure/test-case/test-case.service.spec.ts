import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { TestCaseService } from './test-case.service';
import { TestCase } from './test-case.model';

describe('Service: TestCase', () => {
    let testCaseService: TestCaseService;
    let mockBackend: MockBackend; 

    let testCasesToQuery: TestCase[] = [
        { TestCaseId:1, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 1", Description: "Test Case 1", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:2, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 2", Description: "Test Case 2", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:3, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 3", Description: "Test Case 3", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:4, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 4", Description: "Test Case 4", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:5, TestSuiteId: 2, TestSuite: "Test Suite 2", Name: "Test Case 5", Description: "Test Case 5", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:6, TestSuiteId: 2, TestSuite: "Test Suite 2", Name: "Test Case 6", Description: "Test Case 6", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:7, TestSuiteId: 2, TestSuite: "Test Suite 2", Name: "Test Case 7", Description: "Test Case 7", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() },
        { TestCaseId:8, TestSuiteId: 2, TestSuite: "Test Suite 2", Name: "Test Case 8", Description: "Test Case 8", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() }
        
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                TestCaseService,
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

        testCaseService = getTestBed().get(TestCaseService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get test cases by test plan', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(testCasesToQuery)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.getByTestPlan("1").subscribe(response =>{   
            let testCases = JSON.parse(JSON.stringify(response.Data as any));
            expect(testCases.length).toEqual(8);
        });         
    }));

    it('Should return error 500 on get test cases by test plan', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testCases: TestCase[] = []
        let errorInfo: string;
        testCaseService.getByTestPlan("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
	
	it('Should get testCase by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let testCase = testCasesToQuery.find(c => c.TestCaseId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(testCase)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.getById("1").subscribe(response =>{   
            let testCase = JSON.parse(JSON.stringify(response.Data as any));
            expect(testCase.TestCaseId).toEqual(1);
            expect(testCase.Name).toEqual("Test Case 1");

        });         
    }));

    it('Should not get testCase by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"Test Case not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("Test Case not Found");
            let testCase = JSON.parse(JSON.stringify(response.Data as any));
            expect(testCase).toBeNull();            
        });         
    }));

    it('Return error 500 on get testCase by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testCases: TestCase[] = []
        let errorInfo: string;
        testCaseService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should insert new testCase', async(() =>{        
        let testCase: TestCase;
        testCase = {TestCaseId: 99, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 1", Description: "Test Case 1", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 201
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.insert(testCase).subscribe(response =>{   
            expect(response.status).toEqual(201);    
        });         
    }));

    it('Return error 500 on insert', async(() =>{        
        let testCase: TestCase = {TestCaseId: 99, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 1", Description: "Test Case 1", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testCases: TestCase[] = []
        let errorInfo: string;
        testCaseService.insert(testCase).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should update testCase', async(() =>{        
        let testCase: TestCase;
        testCase = {TestCaseId: 99, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 1", Description: "Test Case 1", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() }
        mockBackend.connections.subscribe((conn:MockConnection) => {
            
            
            let options = new ResponseOptions({                
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.update(testCase).subscribe(response =>{   
            expect(response.status).toEqual(204);    
        });         
    }));

    it('Return error 500 on update', async(() =>{        
        let testCase: TestCase = {TestCaseId: 99, TestSuiteId: 1, TestSuite: "Test Suite 1", Name: "Test Case 1", Description: "Test Case 1", PreConditions: "Pre Conditions", PosConditions: "Pos Conditions", ExpectedResult: "Expected Result", Status: true, Type: "Principal", CreatedBy: 1, CreatedName: "Administrador Sistema", CreatedDate: new Date(), LastUpdatedBy: 1, LastUpdatedName: "Administrador Sistema", LastUpdatedDate: new Date() }

        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let testCases: TestCase[] = []
        let errorInfo: string;
        testCaseService.update(testCase).subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should change status', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let testCase = testCasesToQuery.find(c => c.TestCaseId.toString() == "1");
            
            let options = new ResponseOptions({
                status: 204
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.changeStatus("1").subscribe(response =>{   
            expect(response.status).toEqual(204);    

        });         
    }));

    it('Should not changestatus by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"TestCase not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        testCaseService.changeStatus("1").subscribe(
            response =>response,
            error =>{
                expect(error.status).toEqual(404);
                expect(error.Message).toEqual("TestCase not Found");
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

        let testCases: TestCase[] = []
        let errorInfo: string;
        testCaseService.changeStatus("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));
});