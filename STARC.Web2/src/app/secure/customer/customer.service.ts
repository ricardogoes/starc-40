import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

@Injectable()
export class CustomerService {

	constructor(
		private authService : AuthService
	){}

	getAll() : Observable<any>{	
		
		return this.authService.authGet("/Customers")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
	}

	getActive() : Observable<any>{
		return this.authService.authGet("/Customers/Active")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	getById(customerId: string) : Observable<any>{
		return this.authService.authGet("/Customers/" + customerId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	insert(customer: any) : Observable<any> {
		return this.authService.authPost("/Customers", JSON.stringify(customer))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	update(customer: any) : Observable<any>{
		return this.authService.authPut("/Customers/" + customer.CustomerId, JSON.stringify(customer))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	changeStatus(customerId: number) : Observable<any> {
		return this.authService.authPut("/Customers/" + customerId + "/ChangeStatus", "")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}
}