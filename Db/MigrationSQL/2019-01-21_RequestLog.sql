info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.1.4-rtm-31024 initialized 'Dbc' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: None
CREATE TABLE [RequestLogs] (
    [RequestLogId] int NOT NULL IDENTITY,
    [DateCreated] datetime2 NOT NULL DEFAULT (getdate()),
    [URL] nvarchar(2048) NOT NULL DEFAULT N'',
    [RequestMethod] nvarchar(64) NOT NULL DEFAULT N'',
    [RequestContentType] nvarchar(256) NOT NULL DEFAULT N'',
    [RequestText] nvarchar(max) NULL,
    [ResponseStatus] int NOT NULL,
    [ResponseContentType] nvarchar(256) NOT NULL DEFAULT N'',
    [ResponseSize] int NOT NULL,
    [ResponseMs] decimal(18, 4) NOT NULL,
    [ResponseType] int NOT NULL DEFAULT 0,
    [ResponseText] nvarchar(max) NOT NULL DEFAULT N'',
    [IP] nvarchar(64) NOT NULL DEFAULT N'',
    [UserAgent] nvarchar(256) NOT NULL DEFAULT N'',
    [Referer] nvarchar(2048) NOT NULL DEFAULT N'',
    [Location] nvarchar(2048) NOT NULL DEFAULT N'',
    CONSTRAINT [PK_RequestLogs] PRIMARY KEY ([RequestLogId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190117041629_RequestLog', N'2.1.4-rtm-31024');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLogs]') AND [c].[name] = N'RequestText');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [RequestLogs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [RequestLogs] ALTER COLUMN [RequestText] nvarchar(max) NOT NULL;
ALTER TABLE [RequestLogs] ADD DEFAULT N'' FOR [RequestText];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190117044643_RequestLogFix', N'2.1.4-rtm-31024');

GO

ALTER TABLE [RequestLogs] DROP CONSTRAINT [PK_RequestLogs];

GO

ALTER TABLE [Messages] DROP CONSTRAINT [PK_Messages];

GO

EXEC sp_rename N'[RequestLogs]', N'RequestLog';

GO

EXEC sp_rename N'[Messages]', N'Message';

GO

ALTER TABLE [RequestLog] ADD CONSTRAINT [PK_RequestLog] PRIMARY KEY ([RequestLogId]);

GO

ALTER TABLE [Message] ADD CONSTRAINT [PK_Message] PRIMARY KEY ([MessageId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190117045022_SingularizeTables', N'2.1.4-rtm-31024');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLog]') AND [c].[name] = N'ResponseText');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [RequestLog] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [RequestLog] ALTER COLUMN [ResponseText] nvarchar(max) NULL;
ALTER TABLE [RequestLog] ADD DEFAULT N'' FOR [ResponseText];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLog]') AND [c].[name] = N'ResponseStatus');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [RequestLog] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [RequestLog] ALTER COLUMN [ResponseStatus] int NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLog]') AND [c].[name] = N'ResponseSize');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [RequestLog] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [RequestLog] ALTER COLUMN [ResponseSize] int NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLog]') AND [c].[name] = N'ResponseMs');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [RequestLog] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [RequestLog] ALTER COLUMN [ResponseMs] decimal(18, 4) NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RequestLog]') AND [c].[name] = N'ResponseContentType');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [RequestLog] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [RequestLog] ALTER COLUMN [ResponseContentType] nvarchar(256) NULL;
ALTER TABLE [RequestLog] ADD DEFAULT N'' FOR [ResponseContentType];

GO

ALTER TABLE [RequestLog] ADD [RequestBegin] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [RequestLog] ADD [RequestSize] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190120210437_RequestLogUpdate', N'2.1.4-rtm-31024');

GO


