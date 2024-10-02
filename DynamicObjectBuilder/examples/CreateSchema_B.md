mutation CreateB($Schema_A_Id: UUID!) {
  createSchema(
    input: {
      request: {
        schemaName: "B"
        fields: [
          { fieldType: 0, isRequired: true, name: "Name" }
          {
            fieldType: 4
            isRequired: true
            name: "Deep 1"
            dynamicSchemaId: $Schema_A_Id
          }
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
    "Schema_A_Id": ""
}