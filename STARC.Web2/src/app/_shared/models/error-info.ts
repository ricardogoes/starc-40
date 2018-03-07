import {Component, OnInit, Input } from '@angular/core';
import { Response} from "@angular/http";
import {Observable} from "rxjs";

export class ErrorInfo {
    message: string;
    response: Response = null;

    parsePromiseResponseError(response: any) {

        if (response.hasOwnProperty("message"))
            return Promise.reject(response);
        if (response.hasOwnProperty("Message")) {
            response.message = response.Message;
            return Promise.reject(response);
        }

        let err = new ErrorInfo();
        err.response = response;
        err.message = response.statusText;

        try {
            let data = response.json();
            if (data && data.message)
                err.message = data.message;
        }
        catch(ex) {

        }

        return Promise.reject(err);
    }

    parseObservableResponseError(response: any):Observable<any> {
        
        if (response.hasOwnProperty("message")){
            return Observable.throw(response);
        }
        if (response.hasOwnProperty("Message")) {
            response.message = response.Message;
            return Observable.throw(response);
        }

        let err = new ErrorInfo();
        err.response = response;
        err.message = response.statusText;        

        try {
            let data = response.json();

            if (data && data.Data){
                err.message = "";                
                for(var message of data.Data)
                {                
                    err.message += message + " ";
                }
            } else if(data && data.Message){
                err.message = data.Message;
            }            
        }        
        catch(ex) { }

        if (!err.message)
            err.message = "Unknown server failure, please try again or contact system administrator.";
        
        return Observable.throw(err);
    }
}