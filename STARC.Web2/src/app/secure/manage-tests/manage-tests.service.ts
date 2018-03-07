import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

import { ValidationResult } from '../../_shared/models/validation-result.model';


 
@Injectable()
export class ManageTestsService {
	
	constructor(
		private authService : AuthService		
	){}

	getTree(testPlanId: string) {
		return JSON.parse('{"value":"Test Plan - 16/08/2017","id":1,"settings":{"cssClasses":{"expanded":"fa fa-caret-down","collapsed":"fa fa-caret-right","empty":"fa fa-caret-right disabled","leaf":"fa"},"templates":{"node":"<i class=\\"fa fa-book\\"></i>","leaf":"<i class=\\"fa fa-file-text-o\\"></i>"}},"children":[]}');
	}
}