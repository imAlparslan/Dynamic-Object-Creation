IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SchemaBuilder')
BEGIN
    CREATE DATABASE SchemaBuilder;
END;
GO

USE SchemaBuilder;

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [DynamicEntity] (
    [DynamicEntityId] uniqueidentifier NOT NULL,
    [SchemaId] uniqueidentifier NOT NULL,
    [Fields] nvarchar(max) NULL,
    CONSTRAINT [PK_DynamicEntity] PRIMARY KEY ([DynamicEntityId])
);
GO

CREATE TABLE [DynamicSchema] (
    [DynamicSchemaId] uniqueidentifier NOT NULL,
    [DynamicSchemaName] nvarchar(450) NOT NULL,
    [IsCoreSchema] bit NOT NULL,
    [Fields] nvarchar(max) NULL,
    CONSTRAINT [PK_DynamicSchema] PRIMARY KEY ([DynamicSchemaId])
);
GO

CREATE UNIQUE INDEX [IX_DynamicSchema_DynamicSchemaName] ON [DynamicSchema] ([DynamicSchemaName]);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] ON;
INSERT INTO [DynamicSchema] ([DynamicSchemaId], [DynamicSchemaName], [IsCoreSchema])
VALUES ('0457e742-58e6-4136-b6e9-d7df38f031d9', N'Text', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] ON;
INSERT INTO [DynamicSchema] ([DynamicSchemaId], [DynamicSchemaName], [IsCoreSchema])
VALUES ('239b81ac-ae05-4647-aa14-4633a3678561', N'Number', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] ON;
INSERT INTO [DynamicSchema] ([DynamicSchemaId], [DynamicSchemaName], [IsCoreSchema])
VALUES ('98b78268-5150-4bfc-ac68-bdf3f24177a2', N'Decimal', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] ON;
INSERT INTO [DynamicSchema] ([DynamicSchemaId], [DynamicSchemaName], [IsCoreSchema])
VALUES ('21ce8644-3857-49dc-a1c6-fcf29341e9a4', N'Boolean', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'DynamicSchemaId', N'DynamicSchemaName', N'IsCoreSchema') AND [object_id] = OBJECT_ID(N'[DynamicSchema]'))
    SET IDENTITY_INSERT [DynamicSchema] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240926111533_Database', N'8.0.8');
GO

COMMIT;
GO

