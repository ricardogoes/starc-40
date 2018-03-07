import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

import { TestCase } from './test-case.model';

@Injectable()
export class TestCaseService {
	constructor(
		private authService : AuthService
    ){}

	getByTestPlan(testPlanId: string) : Observable<any>{
		return this.authService.authGet("/TestCases/ByTestPlan/" + testPlanId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	getByTestSuite(testSuiteId: string) : Observable<any>{
		//TODO: Add tests
		return this.authService.authGet("/TestCases/ByTestSuite/" + testSuiteId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	getById(testCaseId: string) : Observable<any>{
		return this.authService.authGet("/TestCases/" + testCaseId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	insert(testCase: any) : Observable<any> {

		return this.authService.authPost("/TestCases", JSON.stringify(testCase))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	update(testCase: any) : Observable<any> {
		return this.authService.authPut("/TestCases/" + testCase.TestCaseId, JSON.stringify(testCase))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	changeStatus(testCaseId: string) : Observable<any> {
		return this.authService.authPut("/TestCases/" + testCaseId + '/ChangeStatus', "")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}
}