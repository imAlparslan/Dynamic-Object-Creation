using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using DynamicObjectBuilder.DataAccess.Models.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace DynamicObjectBuilder.Business.Common;
internal static class SqlHelper
{
    private const string validRagexPattern = @"^[A-Za-z0-9 ]+$";

    public static string SchemaNameToSqlTableName(string schemaName)
    {
        if (Regex.IsMatch(schemaName, validRagexPattern))
        {
            return schemaName.Trim().Replace(" ", "_");
        };

        throw new SchemaException($"'{schemaName}' is invalid schema name");
    }

    public static string FieldNameToSqlColumnName(string fieldName)
    {
        if (Regex.IsMatch(fieldName, validRagexPattern))
        {
            return fieldName.Trim().Replace(" ", "_");
        };

        throw new SchemaException($"'{fieldName}' is invalid field name");
    }

    public static string FieldTypeToSqlDataType(FieldType fieldType)
    {
        return fieldType switch
        {
            FieldType.BOOLEAN => "bit",
            FieldType.TEXT => "nvarchar(max)",
            FieldType.NUMBER => "int",
            FieldType.DECIMAL => "float",
            FieldType.Dynamic => "uniqueidentifier",
            _ => throw new SchemaException("Unknown field type")
        };
    }

    public static string IsRequiredToSql(bool isRequired)
    {
        return isRequired switch
        {
            true => "not null",
            false => "null"
        };
    }

    public static string FieldToSql(SchemaField field)
    {
        var columnName = FieldNameToSqlColumnName(field.Name);
        var sqlDataType = FieldTypeToSqlDataType(field.FieldType);
        var sqlNullable = IsRequiredToSql(field.IsRequired);

        return $"{columnName} {sqlDataType} {sqlNullable}";
    }

    public static string DynamicSchemaToRawSql(DynamicSchema schema)
    {

        StringBuilder query = new StringBuilder();

        var columns = schema.Fields.Select(FieldToSql)
            .ToList();

        query.AppendFormat("CREATE TABLE {0}", schema.Name);
        query.AppendLine("(Id uniqueidentifier NOT NULL,");
        //other columns
        query.AppendJoin(",\n", columns);
        query.AppendLine(",");

        query.AppendFormat("CONSTRAINT[PK_{0}] PRIMARY KEY([Id]))", schema.Name);

        //TODO: Add foreign keys

        return query.ToString();
    }

    public static string SqlTableNameToSchemaName(string tableName)
    {
        return tableName.Replace("_", " ");
    }

    public static string SqlColumnNameToFieldName(string columnName)
    {
        return columnName.Replace("_", " ");
    }
}
