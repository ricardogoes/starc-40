import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

@Injectable()
export class UserService {

	constructor(
		private authService : AuthService
	){}

	getById(userId: string) : Observable<any>{
		return this.authService.authGet("/Users/" + userId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	getByCustomer(customerId: string) : Observable<any>{
		return this.authService.authGet("/Users/ByCustomer/" + customerId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	getByNotInProject(projectId: string) : Observable<any>{
		return this.authService.authGet("/Users/ByNotInProject/" + projectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	insert(user: any) : Observable<any> {

		return this.authService.authPost("/Users", JSON.stringify(user))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	update(user: any) : Observable<any> {
		return this.authService.authPut("/Users/" + user.UserId, JSON.stringify(user))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	changeStatus(userId: number) : Observable<any> {
		return this.authService.authPut("/Users/" + userId + '/ChangeStatus', "")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}
}