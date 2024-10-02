using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicObjectBuilder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicSchema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchemaField",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldIdentifer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    DynamicSchemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaField_DynamicSchema_DynamicSchemaId",
                        column: x => x.DynamicSchemaId,
                        principalTable: "DynamicSchema",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchemaField_DynamicSchema_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "DynamicSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicSchema_Name",
                table: "DynamicSchema",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchemaField_DynamicSchemaId",
                table: "SchemaField",
                column: "DynamicSchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaField_OwnerId",
                table: "SchemaField",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchemaField");

            migrationBuilder.DropTable(
                name: "DynamicSchema");
        }
    }
}
