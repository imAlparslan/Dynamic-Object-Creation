mutation CreateA {
  createSchema(
    input: {
      request: {
        schemaName: "A"
        fields: [
         ## { fieldType: 0, isRequired: true, name: "Name" },
          { fieldType: 0, isRequired: true, name: "Some info 1" },
          { fieldType: 0, isRequired: true, name: "Some info 2" },
         ## { fieldType: 3, isRequired: true, name: "Some decimal" },

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
