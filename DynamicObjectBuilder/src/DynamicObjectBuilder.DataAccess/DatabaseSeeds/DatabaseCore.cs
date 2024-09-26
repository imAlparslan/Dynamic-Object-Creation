using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicObjectBuilder.DataAccess.DatabaseSeeds;
internal static class DatabaseCore
{

    public static MigrationBuilder AddCoreSchemas(this MigrationBuilder migrationBuilder)
    {

        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.Parse("0457e742-58e6-4136-b6e9-d7df38f031d9"), "Text", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.Parse("239b81ac-ae05-4647-aa14-4633a3678561"), "Number", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.Parse("98b78268-5150-4bfc-ac68-bdf3f24177a2"), "Decimal", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.Parse("21ce8644-3857-49dc-a1c6-fcf29341e9a4"), "Boolean", true });

        return migrationBuilder;
    }
}
