import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';


@Injectable()
export class UsersInProjectsService {

	constructor(
		private authService : AuthService
	){}

	getById(userInProjectId: string) : Observable<any>{
		return this.authService.authGet("/UsersInProjects/" + userInProjectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	getByUser(userId: string) : Observable<any>{
		return this.authService.authGet("/UsersInProjects/ByUser/" + userId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	getByProject(projectId: string) : Observable<any>{
		return this.authService.authGet("/UsersInProjects/ByProject/" + projectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	getByCustomer(customerId: string) : Observable<any>{
		return this.authService.authGet("/UsersInProjects/ByCustomer/" + customerId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	insert(user: any) : Observable<any> {

		return this.authService.authPost("/UsersInProjects", JSON.stringify(user))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	delete(userInProjectId: string) : Observable<any> {
		return this.authService.authDelete("/UsersInProjects/" + userInProjectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}
}