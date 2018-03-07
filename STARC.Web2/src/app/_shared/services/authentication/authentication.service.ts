import { Injectable } from "@angular/core";
import { Headers, Http, RequestOptions } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';

import {AppSettings} from '../../../app.settings';

import { ApiResponse } from "../../models/api-response.model";

import {MenusNotAllowedToCustomerAdmin, MenusNotAllowedToUser} from './authentication.access'

@Injectable()
export class AuthService {
    private tokeyKey = "token";
    private token: string;

    constructor(
        private http: Http   
    ) { }

    login(userName: string, password: string): Observable<ApiResponse> {      
        return this.http.post(AppSettings.API_ENDPOINT + "/TokenAuth", { Username: userName, Password: password })
            .map(response => {                
                let result = response.json() as ApiResponse;                
                if (result.State == 1) {
                    let json = result.Data as any;

                    this.loadSessions(json);
                }
                return result;
            })
            .catch(this.handleError);
    }

    logout() {
        sessionStorage.clear();        
    }

    checkLogin(): boolean {
        var token = sessionStorage.getItem(this.tokeyKey);
        return token != null;
    }

    getLoggedUserInfo(){
        return JSON.parse(sessionStorage.getItem("loggedUser"));
    }

    hasAuthority(menu: string): boolean{
        var user = this.getLoggedUserInfo();        
        
        switch(user.UserProfileId){
            case 2:
                return this.verifyMenusAllowedToCustomerAdmin(menu);
            case 3:
                return this.verifyMenusAllowedToUser(menu);
            default:
                return true;
        }     
    }

    authPost(url: string, body: any): Observable<ApiResponse> {
        let headers = this.initAuthHeaders();

        return this.http.post(AppSettings.API_ENDPOINT +  url, body, { headers: headers })
            .map(response => response)
            .catch(this.handleError);
    }

    authPut(url: string, body: any): Observable<ApiResponse> {
        let headers = this.initAuthHeaders();
                
        return this.http.put(AppSettings.API_ENDPOINT +  url, body, { headers: headers })
            .map(response => response)
            .catch(this.handleError);
    }

     authPatch(url: string, body: any): Observable<ApiResponse> {
        let headers = this.initAuthHeaders();
                
        return this.http.patch(AppSettings.API_ENDPOINT +  url, body, { headers: headers })
            .map(response => response)
            .catch(this.handleError);
    }

    authDelete(url: string): Observable<ApiResponse> {
        let headers = this.initAuthHeaders();

        return this.http.delete(AppSettings.API_ENDPOINT +  url, { headers: headers })
            .map(response => response)
            .catch(this.handleError);
    }

    authGet(url: string): Observable<ApiResponse> {
        let headers = this.initAuthHeaders();

        return this.http.get(AppSettings.API_ENDPOINT +  url, { headers: headers })
            .map(response => response.json() as ApiResponse)
            .catch(this.handleError);
    }

    private loadSessions(json: any){                
        sessionStorage.setItem("token", json.accessToken);
        sessionStorage.setItem("loggedUser", JSON.stringify(json.loggedUser));

        var user = this.getLoggedUserInfo();

        if(user.UserProfileId == 2)
        {
            sessionStorage.setItem("selectedCustomerId", user.CustomerId.toString());
        }        
    }
    
    private verifyMenusAllowedToCustomerAdmin(menu: string) : boolean {
        for(var menuOption of MenusNotAllowedToCustomerAdmin){
            if(menu == menuOption)
                return false;
        }

        return true;
    }

    private verifyMenusAllowedToUser(menu: string) : boolean{
        for(var menuOption of MenusNotAllowedToUser){
            if(menu == menuOption)
                return false;
        }

        return true;
    }

    private getLocalToken(): string {
        if (!this.token) {
            this.token = sessionStorage.getItem(this.tokeyKey);
        }
        return this.token;
    }

    private initAuthHeaders(): Headers {
        let token = this.getLocalToken();
        if (token == null) throw "No token";

        var headers = new Headers();
        headers.append("Authorization", "Bearer " + token);
        headers.append("Content-Type", "application/json");        

        return headers;
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error.message);
        return Promise.reject(error.message || error);
    }
}