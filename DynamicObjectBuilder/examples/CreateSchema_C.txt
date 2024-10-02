mutation CreateC ($Schema_B_Id: UUID!) {
  createSchema(
    input: {
      request: {
        schemaName: "C"
        fields: [
          { fieldType: 0, isRequired: true, name: "Name" },
                
          { fieldType: 4, isRequired: true, name: "Deep 2",dynamicSchemaId:$Schema_B_Id },

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
    "Schema_B_Id": ""
}