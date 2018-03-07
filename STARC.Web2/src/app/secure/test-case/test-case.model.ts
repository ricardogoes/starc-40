export class TestCase {
    public TestCaseId: number;    
    public Name: string;
    public Type: string;    
    public Description: string;
    public PreConditions: string;
    public PosConditions: string;
    public ExpectedResult: string;     
    public Status: boolean;
    public TestSuiteId: number;
    public TestSuite: string;   
    public CreatedBy: number;
    public CreatedName: string;
    public CreatedDate: Date;
    public LastUpdatedBy: number;
    public LastUpdatedName: string;
    public LastUpdatedDate: Date; 
}