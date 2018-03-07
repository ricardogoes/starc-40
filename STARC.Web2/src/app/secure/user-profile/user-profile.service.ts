import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

@Injectable()
export class UserProfileService {
	
	constructor(
		 private authService: AuthService
	){ }

	getAll() : Observable<any>{		
		return this.authService.authGet("/UserProfiles")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	getById(userProfileId: string) : Observable<any>{
		return this.authService.authGet("/UserProfiles/" + userProfileId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}
}