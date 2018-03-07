import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from './authentication.service';


describe('Service: AuthService', () => {
    let authService: AuthService;
    let mockBackend: MockBackend;    

    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
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

        authService = getTestBed().get(AuthService);
        mockBackend = getTestBed().get(MockBackend);

        
    });

    afterEach(() => {
        sessionStorage.clear();
    })

    it("Should login with success - System Administrator", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin","UserProfileId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin", "admin").subscribe((response) => {
            
            expect(response.State).toEqual(1);

            let data = (response.Data as any);
            expect(data.loggedUser.UserId).toEqual(1);
            expect(data.loggedUser.Username).toEqual("admin");
            expect(sessionStorage.getItem("token")).toEqual("unitTest");            
        });        
    }));

    it("Should login with success - Customer Administrator", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {
            
            expect(response.State).toEqual(1);

            let data = (response.Data as any);
            expect(data.loggedUser.UserId).toEqual(1);
            expect(data.loggedUser.Username).toEqual("admin.customer");
            expect(sessionStorage.getItem("token")).toEqual("unitTest");            
            expect(sessionStorage.getItem("selectedCustomerId")).toEqual("1");     
        });        
    }));


    it("Should not login - invalid username and password", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1, "Message":"Username or password is invalid", "Data":null}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {
            
            expect(response.State).toEqual(-1);
            expect(response.Message).toEqual("Username or password is invalid");
            expect(sessionStorage.getItem("token")).toBeNull();           
        });        
    }));

    it("Should logout with success", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            expect(sessionStorage.length).toBeGreaterThan(0);

            authService.logout();
            expect(sessionStorage.length).toEqual(0);
        });        
    }));

    it("Should login be checked", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            expect(sessionStorage.length).toBeGreaterThan(0);

            let loginChecked = authService.checkLogin();
            expect(loginChecked).toBeTruthy();
        });        
    }));

    it("Should login be checked", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            expect(sessionStorage.length).toBeGreaterThan(0);

            authService.logout();
            expect(sessionStorage.length).toEqual(0);

            let loginChecked = authService.checkLogin();
            expect(loginChecked).toBeFalsy();
        });        
    }));

    it("Should get info from logged user", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            expect(sessionStorage.length).toBeGreaterThan(0);

            let user = authService.getLoggedUserInfo();
            expect(JSON.stringify(user)).toEqual('{"UserId":1,"Username":"admin.customer","UserProfileId":2,"CustomerId":1}');
        });        
    }));

    it("Should Customer Admin does not have authority on Configuration", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Configuration");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should Customer Admin does not have authority on Customers", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"admin.customer","UserProfileId":2, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("admin.customer", "admin").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Customers");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should User does not have authority on Configuration", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"user","UserProfileId":3, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("user", "user").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Configuration");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should User does not have authority on Customers", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"user","UserProfileId":3, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("user", "user").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Customers");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should User does not have authority on Administration", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"user","UserProfileId":3, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("user", "user").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Administration");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should User does not have authority on Projects", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"user","UserProfileId":3, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("user", "user").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Projects");
            expect(hasAuthority).toBeFalsy();
        });        
    }));

    it("Should User does not have authority on Users", async(() =>{
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1, "Message":null, "Data":{"accessToken":"unitTest","loggedUser":{"UserId":1, "Username":"user","UserProfileId":3, "CustomerId":1}}}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });
        
        authService.login("user", "user").subscribe((response) => {            
            let hasAuthority = authService.hasAuthority("Users");
            expect(hasAuthority).toBeFalsy();
        });        
    }));
});