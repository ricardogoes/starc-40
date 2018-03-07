import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

import { ValidationResult } from '../../_shared/models/validation-result.model';

@Injectable()
export class ProjectService {

	constructor(
		private authService : AuthService
	){}

	getActiveByCustomer(customerId: string) : Observable<any>{
		return this.authService.authGet("/Projects/Active/ByCustomer/" + customerId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
		
	}

	getById(projectId: string) : Observable<any>{
		return this.authService.authGet("/Projects/" + projectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	getByCustomer(customerId: string) : Observable<any>{
		return this.authService.authGet("/Projects/ByCustomer/" + customerId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	insert(project: any) : Observable<any> {

		return this.authService.authPost("/Projects", JSON.stringify(project))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	update(project: any) : Observable<any> {
		return this.authService.authPut("/Projects/" + project.ProjectId, JSON.stringify(project))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	changeStatus(projectId: number) : Observable<any> {
		return this.authService.authPut("/Projects/" + projectId + '/ChangeStatus', "")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	validateStartAndFinishDate(project: any) : ValidationResult{
		var validation = new ValidationResult();

		if(project.StartDate > project.FinishDate){
			validation.Status = false;
			validation.Message = "Finish Date must be greater than Start Date";
		} else{
			validation.Status = true;
			validation.Message = "";
		}

		return validation;
	}
}