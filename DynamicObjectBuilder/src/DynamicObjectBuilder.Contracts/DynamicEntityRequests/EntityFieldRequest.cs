namespace DynamicObjectBuilder.Contracts.DynamicEntityRequests;
public class EntityFieldRequest
{
    public Guid SchemaTypeId { get; private set; }
    public Guid FieldIdentifier { get; private set; }
    public EntityFieldValueRequest Value { get; set; }

    public EntityFieldRequest(Guid schemaTypeId, Guid fieldIdentifier, EntityFieldValueRequest value)
    {
        SchemaTypeId = schemaTypeId;
        FieldIdentifier = fieldIdentifier;
        Value = value;
    }

    private EntityFieldRequest()
    {

    }
}


/*


InsertCompany:
    SchemaId
    Fields
        Company Name
        Fields
            Company Name : Company A
            Product Name : Product A
            Product Code : Product Code
            Employees: 
                [
                    {
                        "Employee":
                            Fields: 
                            {
                                Employee Name : Employee A,
                                Employee Dep : Department A
                            }
                    },
                    {
                        "Employee":
                        {
                            Fields
                            {
                                Employee Name : Employee B,
                                Employee Dep : Department B
                            }
                        }
                    }                
]
            


Company:
    SchemaName : Company 
    Fields 
        Company Name: string
        Product : Product
                    Fields:
                        {
                            Product Name : string    
                            Product Code : string      
                        }
       Employees: List<Employee>
                    [
                        {
                        Fields: 
                            Employee Name: string
                            Employee Dep: string
                        },
                        Fields: 
                            EmployeeName: string
                            EmployeeDepartment: string
                        }
                    ]
            
        
SchemaFiled
    {
        Schema Name : String
        FieldInfo
            IsRequired
            IsCore
            IsList
            FieldCorelationId
    }
    

 
*/