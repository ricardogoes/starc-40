// Imports
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TreeModel, NodeEvent, Ng2TreeSettings, RenamableNode } from '../../_shared/directives/ng2-tree';

import { AlertService } from '../../_shared/directives/alert-notification/alert.service';

import { TestPlanService } from '../test-plan/test-plan.service';
import { TestPlan } from '../test-plan/test-plan.model';

import { TestCaseService } from '../test-case/test-case.service';
import { TestCase } from '../test-case/test-case.model';

import { TestSuiteService } from '../test-suite/test-suite.service'
import { TestSuite } from '../test-suite/test-suite.model';

import { ManageTestsService } from './manage-tests.service'; 

@Component({
  selector: 'manage-tests',
  templateUrl: 'manage-tests.component.html',    
  providers: [AlertService, ManageTestsService, TestPlanService, TestCaseService, TestSuiteService],
})

export class ManageTestsComponent implements OnInit{
  title = 'Manage Tests';  
  tree: TreeModel;
  testPlans: TestPlan[];
  selectedTestPlanId: string;
  testCaseForm: FormGroup;
  testSuiteForm: FormGroup;
  testCase: TestCase;
  testCaseId: number;
  isTestCase: boolean;
  testSuite: TestSuite;
  testSuiteId: number;
  isTestSuite: boolean;

  constructor(
    private fb: FormBuilder, 
    private alertService: AlertService,
    private manageTestsService: ManageTestsService,
    private testPlanService: TestPlanService,
    private testCaseService: TestCaseService,
    private testSuiteService: TestSuiteService
  ){}
  
  public settings: Ng2TreeSettings = {
    rootIsVisible: false
  };

  ngOnInit() {
    this.alertService.clear();    
    this.loadActiveTestPlans();
  }

  loadActiveTestPlans(){
    let projectId: string = sessionStorage.getItem("selectedProjectId");

    this.testPlanService.getActiveByProject(projectId)
      .subscribe(
        response => {
          this.testPlans = JSON.parse(JSON.stringify(response.Data as any)); 
        }, err => {
          this.alertService.error(err.message);
        });
  }

  changeSelectedTestPlan(selectedValue: string){
    this.selectedTestPlanId = selectedValue;
    this.treeLoad();
  }

  treeLoad(){
    //TODO: carregar automaticamente    
    this.testPlanService.createTestPlanStructure(this.selectedTestPlanId)
      .subscribe(
        response => {
          //console.log(response);
          //this.tree = JSON.parse(response);
        }, err => {
          this.alertService.error(err.message);
        });    
  }

  buildTestCaseForm(): void {    
    this.testCaseForm = this.fb.group({
        'Name': [
            this.testCase.Name, [
                Validators.required
            ]
        ],
        'Description': [
            this.testCase.Description,  [
                Validators.required
            ]
        ],
        'TestCaseType': [
            this.testCase.Type
        ],
        'PreConditions': [
            this.testCase.PreConditions
        ],
        'PosConditions': [
            this.testCase.PosConditions
        ],
        'ExpectedResult': [
            this.testCase.ExpectedResult
        ],

    });

    this.testCaseForm.valueChanges.subscribe(data => this.onTestCaseValueChanged(data));
    this.onTestCaseValueChanged(); // (re)set validation messages now
      
  }

  onTestCaseValueChanged(data?: any) {
    if (!this.testCaseForm) { return; }
    
    const testCaseform = this.testCaseForm;

    for (const field in this.testCaseFormErrors) {
      const control = testCaseform.get(field);

        // clear previous error message (if any)
      if(control.valid)
          this.testCaseFormErrors[field] = '';

      if (control && control.dirty && !control.valid) {
          const messages = this.testCaseValidationMessages[field];
          for (const key in control.errors) {
              
              this.testCaseFormErrors[field] = messages[key] + ' ';
          }
      }
    }
  }

  startTestCaseErrors() {
    this.testCaseFormErrors.Description = 'Description required';
  }

  testCaseFormErrors = {
    'Name': '',
    'Description': ''
  };

  testCaseValidationMessages = {    
    'Name': {
        'required': 'Name required.'
    },
    'Description': {
        'required': 'Description required.'
    }
  };

  buildTestSuiteForm(): void {    
    this.testSuiteForm = this.fb.group({
        'Name': [
            this.testSuite.Name, [
                Validators.required
            ]
        ],
        'Description': [
            this.testSuite.Description
        ]
    });

    this.testSuiteForm.valueChanges.subscribe(data => this.onTestSuiteValueChanged(data));
    this.onTestSuiteValueChanged(); // (re)set validation messages now
      
  }

  onTestSuiteValueChanged(data?: any) {
    if (!this.testSuiteForm) { return; }
    
    const form = this.testSuiteForm;

    for (const field in this.testSuiteFormErrors) {
      const control = form.get(field);

        // clear previous error message (if any)
      if(control.valid)
          this.testSuiteFormErrors[field] = '';

      if (control && control.dirty && !control.valid) {
          const messages = this.testSuiteValidationMessages[field];
          for (const key in control.errors) {
              
              this.testSuiteFormErrors[field] = messages[key] + ' ';
          }
      }
    }
  }

  testSuiteFormErrors = {
    'Name': ''
  };

  testSuiteValidationMessages = {
    'Name': {
        'required': 'Name required.'
    }
  };

  private loadTestCaseForm(testCaseId: string){
      this.isTestCase = true;
      this.isTestSuite = false;
      //this.testCase = this.testCaseService.getById(testCaseId);

      this.buildTestCaseForm();          
  }

  private loadTestSuiteForm(testSuiteId: string){
    this.isTestSuite = true;
    this.isTestCase = false;
   // this.testSuite = this.testSuiteService.getById(testSuiteId);

    this.buildTestSuiteForm();                  
  }

  private onNodeSelected(e: NodeEvent): void {
    if(e.node.isLeaf()){
      this.loadTestCaseForm(e.node.node.id.toString());
      
    }else{
      this.loadTestSuiteForm(e.node.node.id.toString());
    }
  }

  private onNodeMoved(e: NodeEvent): void {
    alert(e.node.parent.node.id)    
  }

  private onNodeRenamed(e: NodeEvent): void {
    //ManageTestsComponent.logEvent(e, 'Renamed');
  }

  private onNodeCreated(e: NodeEvent): void {
    if(e.node.isLeaf()){
      this.isTestCase = true;
      this.isTestSuite = false;

      this.testCase = new TestCase();
      this.testCase.Name = e.node.node.value.toString();

      this.buildTestCaseForm();
      this.startTestCaseErrors();
      
    }else{
      this.isTestSuite = true;
      this.isTestCase = false;

      this.testSuite = new TestSuite();
      this.testSuite.Name = e.node.node.value.toString();

      this.buildTestSuiteForm();
    }    
  } 

  private changeStatus(testCase: TestCase){

  }


}