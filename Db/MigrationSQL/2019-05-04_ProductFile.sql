CREATE TABLE [File] (
    [FileId] int NOT NULL IDENTITY,
    [FileName] nvarchar(max) NULL,
    [MimeType] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [Content] varbinary(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY ([FileId])
);

GO

CREATE TABLE [Product] (
    [ProductId] int NOT NULL IDENTITY,
    [ProductName] nvarchar(max) NULL,
    [ProductDesc] nvarchar(max) NULL,
    [ProductRichDesc] nvarchar(max) NULL,
    [Active] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId])
);

GO

CREATE TABLE [ProductResource] (
    [ProductResourceId] int NOT NULL IDENTITY,
    [ProductId] int NULL,
    [ResourceName] nvarchar(max) NULL,
    [ResourceInfo] nvarchar(max) NULL,
    [SortOrder] int NOT NULL,
    [ResourceFileId] int NULL,
    [Active] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_ProductResource] PRIMARY KEY ([ProductResourceId]),
    CONSTRAINT [FK_ProductResource_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductResource_File_ResourceFileId] FOREIGN KEY ([ResourceFileId]) REFERENCES [File] ([FileId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ProductResource_ProductId] ON [ProductResource] ([ProductId]);

GO

CREATE INDEX [IX_ProductResource_ResourceFileId] ON [ProductResource] ([ResourceFileId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190505004232_ProductFile', N'2.2.4-servicing-10062');

GO

ALTER TABLE [ProductResource] DROP CONSTRAINT [FK_ProductResource_Product_ProductId];

GO

ALTER TABLE [ProductResource] DROP CONSTRAINT [FK_ProductResource_File_ResourceFileId];

GO

DROP INDEX [IX_ProductResource_ResourceFileId] ON [ProductResource];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'ResourceFileId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProductResource] DROP COLUMN [ResourceFileId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'ResourceName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [ResourceName] nvarchar(128) NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'ResourceInfo');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [ResourceInfo] nvarchar(1024) NOT NULL;

GO

DROP INDEX [IX_ProductResource_ProductId] ON [ProductResource];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'ProductId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [ProductId] int NOT NULL;
CREATE INDEX [IX_ProductResource_ProductId] ON [ProductResource] ([ProductId]);

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'DateCreated');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [DateCreated] datetime2 NOT NULL;
ALTER TABLE [ProductResource] ADD DEFAULT (getdate()) FOR [DateCreated];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'Active');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [Active] bit NOT NULL;
ALTER TABLE [ProductResource] ADD DEFAULT 1 FOR [Active];

GO

ALTER TABLE [ProductResource] ADD [FileId] int NOT NULL DEFAULT 0;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ProductRichDesc');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Product] ALTER COLUMN [ProductRichDesc] nvarchar(2048) NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ProductName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Product] ALTER COLUMN [ProductName] nvarchar(128) NOT NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ProductDesc');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Product] ALTER COLUMN [ProductDesc] nvarchar(1024) NOT NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'DateCreated');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Product] ALTER COLUMN [DateCreated] datetime2 NOT NULL;
ALTER TABLE [Product] ADD DEFAULT (getdate()) FOR [DateCreated];

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'Active');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Product] ALTER COLUMN [Active] bit NOT NULL;
ALTER TABLE [Product] ADD DEFAULT 1 FOR [Active];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[File]') AND [c].[name] = N'MimeType');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [File] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [File] ALTER COLUMN [MimeType] nvarchar(64) NOT NULL;
ALTER TABLE [File] ADD DEFAULT N'' FOR [MimeType];

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[File]') AND [c].[name] = N'FilePath');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [File] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [File] ALTER COLUMN [FilePath] nvarchar(256) NOT NULL;
ALTER TABLE [File] ADD DEFAULT N'' FOR [FilePath];

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[File]') AND [c].[name] = N'FileName');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [File] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [File] ALTER COLUMN [FileName] nvarchar(128) NOT NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[File]') AND [c].[name] = N'DateCreated');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [File] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [File] ALTER COLUMN [DateCreated] datetime2 NOT NULL;
ALTER TABLE [File] ADD DEFAULT (getdate()) FOR [DateCreated];

GO

CREATE INDEX [IX_ProductResource_FileId] ON [ProductResource] ([FileId]);

GO

ALTER TABLE [ProductResource] ADD CONSTRAINT [FK_ProductResource_File_FileId] FOREIGN KEY ([FileId]) REFERENCES [File] ([FileId]) ON DELETE CASCADE;

GO

ALTER TABLE [ProductResource] ADD CONSTRAINT [FK_ProductResource_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190505025442_FixProductFile', N'2.2.4-servicing-10062');

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductResource]') AND [c].[name] = N'ResourceInfo');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [ProductResource] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [ProductResource] ALTER COLUMN [ResourceInfo] nvarchar(1024) NOT NULL;
ALTER TABLE [ProductResource] ADD DEFAULT N'' FOR [ResourceInfo];

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ProductRichDesc');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Product] ALTER COLUMN [ProductRichDesc] nvarchar(2048) NOT NULL;
ALTER TABLE [Product] ADD DEFAULT N'' FOR [ProductRichDesc];

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'ProductDesc');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Product] ALTER COLUMN [ProductDesc] nvarchar(1024) NOT NULL;
ALTER TABLE [Product] ADD DEFAULT N'' FOR [ProductDesc];

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[File]') AND [c].[name] = N'Content');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [File] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [File] ALTER COLUMN [Content] varbinary(max) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190505031543_FixProductFile2', N'2.2.4-servicing-10062');

GO


