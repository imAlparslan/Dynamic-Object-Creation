using System;
using DynamicObjectBuilder.DataAccess.DatabaseSeeds;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicObjectBuilder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SchemaInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicSchema",
                columns: table => new
                {
                    DynamicSchemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DynamicSchemaName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCoreSchema = table.Column<bool>(type: "bit", nullable: false),
                    Fields = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicSchema", x => x.DynamicSchemaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicSchema_DynamicSchemaName",
                table: "DynamicSchema",
                column: "DynamicSchemaName",
                unique: true);

            migrationBuilder.AddCoreSchemas();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicSchema");
        }
    }
}
