mutation CreateSchemaD ($Schema_C_Id: UUID!){
  createSchema(
    input: {
      request: {
        schemaName: "D"
        fields: [
          { fieldType: 0, isRequired: true, name: "Name" },
                
          { fieldType: 4, isRequired: true, name: "Deep 3",dynamicSchemaId: $Schema_C_Id},

        ]
      }
    }
  ) {
    dynamicSchemaResponse {
      id
      name
      fields {
        dynamicSchema {
          name
        }
        fieldType
        id
        isRequired
        name
        owner {
          name
        }
      }
    }
    errors {
      ... on SchemaError {
        message
      }
    }
  }
}

--- Required GraphQL Variables ---

{
    "Schema_C_Id": ""
}