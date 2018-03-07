import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

import { ValidationResult } from '../../_shared/models/validation-result.model';

import { TestSuite } from './test-suite.model';
 
@Injectable()
export class TestSuiteService {
	constructor(
		private authService : AuthService		
	){}

	getById(testSuiteId: string) : Observable<any>{
		return this.authService.authGet("/TestSuites/" + testSuiteId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	getByTestPlan(testPlanId: string) : Observable<any>{		
		//TODO: Add Tests
		return this.authService.authGet("/TestSuites/ByTestPlan/" + testPlanId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	getByParentTestSuite(parentTestSuiteId: string) : Observable<any>{		
		//TODO: Add Tests
		return this.authService.authGet("/TestSuites/ByParentTestSuite/" + parentTestSuiteId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );
	}

	insert(testSuite: any) : Observable<any> {
		return this.authService.authPost("/TestSuites", JSON.stringify(testSuite))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	update(testSuite: any) : Observable<any> {
		return this.authService.authPut("/TestSuites/" + testSuite.TestSuiteId, JSON.stringify(testSuite))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	delete(testSuiteId: string) : Observable<any> {
		return this.authService.authDelete("/TestSuites/" + testSuiteId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}
}