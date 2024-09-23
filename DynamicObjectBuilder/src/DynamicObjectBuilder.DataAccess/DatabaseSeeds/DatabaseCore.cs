using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicObjectBuilder.DataAccess.DatabaseSeeds;
internal static class DatabaseCore
{

    public static MigrationBuilder AddCoreSchemas(this MigrationBuilder migrationBuilder)
    {

        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.NewGuid(), "Text", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.NewGuid(), "Number", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.NewGuid(), "Decimal", true });


        migrationBuilder.InsertData(
            table: "DynamicSchema",
            columns: new[] { "DynamicSchemaId", "DynamicSchemaName", "IsCoreSchema" },
            values: new object[] { Guid.NewGuid(), "Boolean", true });

        return migrationBuilder;
    }
}
