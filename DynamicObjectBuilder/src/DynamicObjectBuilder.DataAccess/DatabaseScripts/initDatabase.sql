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

CREATE TABLE [DynamicSchema] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_DynamicSchema] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SchemaField] (
    [Id] uniqueidentifier NOT NULL,
    [OwnerId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsRequired] bit NOT NULL,
    [FieldType] int NOT NULL,
    [DynamicSchemaId] uniqueidentifier NULL,
    CONSTRAINT [PK_SchemaField] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SchemaField_DynamicSchema_DynamicSchemaId] FOREIGN KEY ([DynamicSchemaId]) REFERENCES [DynamicSchema] ([Id]),
    CONSTRAINT [FK_SchemaField_DynamicSchema_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [DynamicSchema] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_DynamicSchema_Name] ON [DynamicSchema] ([Name]);
GO

CREATE INDEX [IX_SchemaField_DynamicSchemaId] ON [SchemaField] ([DynamicSchemaId]);
GO

CREATE INDEX [IX_SchemaField_OwnerId] ON [SchemaField] ([OwnerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240929165407_DB', N'8.0.8');
GO

COMMIT;
GO

