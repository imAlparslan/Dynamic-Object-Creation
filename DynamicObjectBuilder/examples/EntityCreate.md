mutation CreateNestedEntity(
$Schema_A_Id:UUID!,
$Schema_B_Id:UUID!,
$Schema_C_Id:UUID!,
$Schema_D_Id:UUID!,
$schema_d_deep_3_field_identifier: UUID!,
$schema_c_deep_2_field_identifier: UUID!,
$schema_b_deep_1_field_identifier: UUID!,
$schema_a_some_info_1_field_identifier: UUID!,
$schema_a_some_info_2_field_identifier: UUID!,
$schema_b_name_field_identifier: UUID!,
$schema_c_name_field_identifier: UUID!,
$schema_d_name_field_identifier: UUID!) {
  createEntity(
    input: {
      request: {
        schemaId: $Schema_D_Id
        fields: [
          {
            fieldIdentifier: $schema_d_deep_3_field_identifier
            valueType: 4
            value: {
              entityField: {
                schemaId: $Schema_C_Id
                fields: [
                  {
                    valueType: 4
                    fieldIdentifier: $schema_c_deep_2_field_identifier
                    value: {
                      entityField: {
                        schemaId: $Schema_B_Id
                        fields: [
                          {
                            valueType: 4
                            fieldIdentifier: $schema_b_deep_1_field_identifier
                            value: {
                              entityField: {
                                schemaId: $Schema_A_Id
                                fields: [
                                  {
                                    valueType: 0
                                    fieldIdentifier: $schema_a_some_info_1_field_identifier
                                    value: { text: "A NAME" }
                                  }
                                  {
                                    valueType: 3
                                    fieldIdentifier: $schema_a_some_info_2_field_identifier
                                    value: { decimal: 3.2 }
                                  }
                                ]
                              }
                            }
                          }
                          {
                            fieldIdentifier: $schema_b_name_field_identifier
                            valueType: 0
                            value: { text: "B NAME" }
                          }
                        ]
                      }
                    }
                  }
                  {
                    fieldIdentifier: $schema_c_name_field_identifier
                    valueType: 0
                    value: { text: "C NAME" }
                  }
                ]
              }
            }
          }
          {
            fieldIdentifier: $schema_d_name_field_identifier
            valueType: 0
            value: { text: "D NAME" }
          }
        ]
      }
    }
  ) {
    errors {
      ... on DynamicEntityError {
        message
      }
    }
    uuid
  }
}

--- Required GraphQL Variables ---
{
    "Schema_A_Id":"",
    "Schema_B_Id":"",
    "Schema_C_Id":"",
    "Schema_D_Id":"",
    "schema_d_deep_3_field_identifier":"",
    "schema_c_deep_2_field_identifier":"",
    "schema_b_deep_1_field_identifier":"",
    "schema_a_some_info_1_field_identifier": "",
    "schema_a_some_info_2_field_identifier": "",
    "schema_b_name_field_identifier": "",
    "schema_c_name_field_identifier": "",
    "schema_d_name_field_identifier": ""
}