import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { ErrorInfo } from '../../_shared/models/error-info';

import { ValidationResult } from '../../_shared/models/validation-result.model';

import { TestSuiteService } from '../test-suite/test-suite.service';
import { TestSuite } from '../test-suite/test-suite.model';

import { TestCaseService } from '../test-case/test-case.service';
import { TestCase } from '../test-case/test-case.model';

import { TestPlan } from './test-plan.model';
import { TestPlanStructure } from './test-plan-structure.model';


@Injectable()
export class TestPlanService {

	constructor(		
		private authService : AuthService,
		private testSuiteService: TestSuiteService,
		private testCaseService: TestCaseService
	){}
	
	getByProject(projectId: string) : Observable<any>{
		return this.authService.authGet("/TestPlans/ByProject/" + projectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
		
	}

	getActiveByProject(projectId: string) : Observable<any>{
		return this.authService.authGet("/TestPlans/Active/ByProject/" + projectId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );		
		
	}

	getById(testPlanId: string) : Observable<any>{
		return this.authService.authGet("/TestPlans/" + testPlanId)
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	getStructure(testPlanId: string): Observable<any>{
		//TODO: Add Tests
		return this.authService.authGet("/TestPlans/" + testPlanId + '/Structure')
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	insert(testPlan: any) : Observable<any> {

		return this.authService.authPost("/TestPlans", JSON.stringify(testPlan))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	update(testPlan: any) : Observable<any> {
		return this.authService.authPut("/TestPlans/" + testPlan.TestPlanId, JSON.stringify(testPlan))
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	changeStatus(testPlanId: number) : Observable<any> {
		return this.authService.authPut("/TestPlans/" + testPlanId + '/ChangeStatus', "")
			.map(response => response)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}

	validateStartAndFinishDate(testPlan: any) : ValidationResult{
		var validation = new ValidationResult();

		if(testPlan.StartDate > testPlan.FinishDate){
			validation.Status = false;
			validation.Message = "Finish Date must be greater than Start Date";
		} else{
			validation.Status = true;
			validation.Message = "";
		}

		return validation;
	}

	private createTestPlanNode(testPlan: TestPlan): string{
		let json: string;		
		
		json =  '"value": "'+testPlan.Name+'","id": '+testPlan.TestPlanId+',"settings": {"cssClasses": {"expanded": "fa fa-caret-down","collapsed": "fa fa-caret-right", "empty": "fa fa-caret-right disabled","leaf": "fa"},"templates": {"node": "<i class=\\"fa fa-book\\"></i>","leaf": "<i class=\\"fa fa-file-text-o\\"></i>"}},"children":[';
				
		return json;
	}

	private createTestSuiteNode(nodeType: string, id: string): Observable<any>{
		let testSuites: TestSuite[];
		let structures:  TestPlanStructure[] = [];
		

		switch(nodeType){
			case "Test Plan":
				return this.testSuiteService.getByTestPlan(id)
					.expand(response => {
						testSuites = (response.Data as any)
						//console.log(testSuites.length);
						for(var suite of testSuites){		
							let structure = new TestPlanStructure();					
							structure.value = suite.Name;
							structure.id = suite.TestSuiteId;
							
							if(suite.HasChildren){
								this.createTestSuiteNode("Test Suite", suite.TestSuiteId.toString())
									.subscribe(response => {			
										//console.log(JSON.stringify(response as any));																	
										structure.children = [];
										structure.children.push(response as any);

										structures.push(structure);
										console.log(JSON.stringify(structures));
										return Observable.of(structures);
									});							
							}else {
								structures.push(structure);										
								return Observable.of(structures);
							}
						}			
					})
					//.map(response => response)
					.catch( new ErrorInfo().parseObservableResponseError );	

			case "Test Suite":
				return this.testSuiteService.getByParentTestSuite(id)
					.expand(response => {
						testSuites = (response.Data as any);						

						for(var suite of testSuites){
							let structure = new TestPlanStructure();
							structure.value = suite.Name;
							structure.id = suite.TestSuiteId;

							if(suite.HasChildren){
								this.createTestSuiteNode("Test Suite", suite.TestSuiteId.toString())
									.subscribe(response => {
										structure.children = [];
										structure.children.push(response as any);

										structures.push(structure);
										return Observable.of(structures);
									});
							}else{
								structures.push(structure);										
								return Observable.of(structures);
							}		
						}		
					})
					//.map(response => response)
					.catch( new ErrorInfo().parseObservableResponseError );	
		}
	}

	createTestPlanStructure(testPlanId: string) : Observable<any> {
		let testPlan: TestPlan;
		let treeJSON: string = "{";

		return this.getById(testPlanId)
			.map(response => {
					testPlan = (response.Data as any)
					treeJSON += this.createTestPlanNode(testPlan);
					
					this.createTestSuiteNode("Test Plan", testPlanId)
						.subscribe(structure => {
							//treeJSON += response;
							//treeJSON +=  + "]}"

							//console.log(JSON.stringify(structure));
						})	
				}
			)
			.catch( new ErrorInfo().parseObservableResponseError );	
	}
}