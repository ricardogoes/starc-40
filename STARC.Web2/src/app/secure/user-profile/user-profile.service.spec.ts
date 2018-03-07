import { async, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HttpModule, Http, XHRBackend, ResponseOptions, Response, BaseRequestOptions } from '@angular/http';
import { MockBackend, MockConnection } from '@angular/http/testing';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { UserProfileService } from './user-profile.service';
import { UserProfile } from './user-profile.model';

//import {} from 'angular-moment';

describe('Service: UserProfile', () => {
    let userProfileService: UserProfileService;
    let mockBackend: MockBackend;    

    let userProfilesToQuery: UserProfile[] = [
        { UserProfileId: 1, ProfileName: "Administrador Sistema"},
        { UserProfileId: 2, ProfileName: "Administrador Cliente"},
        { UserProfileId: 3, ProfileName: "Usuário"},
    ]
    beforeEach(() =>{
        TestBed.configureTestingModule({
            imports: [ HttpModule ],
            providers: [ 
                BaseRequestOptions,
                AuthService,
                UserProfileService,
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

        userProfileService = getTestBed().get(UserProfileService);
        mockBackend = getTestBed().get(MockBackend);

        sessionStorage.setItem("token", "unittest");
    });

    it('Should get all userProfiles', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(userProfilesToQuery)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userProfileService.getAll().subscribe(response =>{   
            let userProfiles = JSON.parse(JSON.stringify(response.Data as any));
            expect(userProfiles.length).toEqual(3);
        });         
    }));

    it('Return error 500 on get all active userProfiles', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let userProfiles: UserProfile[] = []
        let errorInfo: string;
        userProfileService.getAll().subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));

    it('Should get user Profile by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let userProfile = userProfilesToQuery.find(c => c.UserProfileId.toString() == "1");
            
            let options = new ResponseOptions({
                body: '{"State":1,"Message":null, "Data":'+JSON.stringify(userProfile)+'}',
                status: 200
            });

            conn.mockRespond(new Response(options));
        });

        userProfileService.getById("1").subscribe(response =>{   
            let userProfile = JSON.parse(JSON.stringify(response.Data as any));
            expect(userProfile.UserProfileId).toEqual(1);
            expect(userProfile.ProfileName).toEqual("Administrador Sistema");

        });         
    }));

    it('Should not get userProfile by invalid id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                body: '{"State":-1,"Message":"UserProfile not Found", "Data":null}',
                status: 404
            });

            conn.mockRespond(new Response(options));
        });

        userProfileService.getById("1").subscribe(response =>{   
            expect(response.Message).toEqual("UserProfile not Found");
            let userProfile = JSON.parse(JSON.stringify(response.Data as any));
            expect(userProfile).toBeNull();            
        });         
    }));

    it('Return error 500 on get userProfile by id', async(() =>{        
    
        mockBackend.connections.subscribe((conn:MockConnection) => {
            let options = new ResponseOptions({
                status: 500,
                statusText: "An error ocurred"
            });

            conn.mockRespond(new Response(options));
        });

        let userProfiles: UserProfile[] = []
        let errorInfo: string;
        userProfileService.getById("1").subscribe(
            response => response
            , error => {
                errorInfo = error.Message;
                expect(errorInfo).toBeDefined();
                expect(errorInfo).toEqual("An error ocurred");
            });
    }));    
});