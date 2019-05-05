IF SCHEMA_ID(N'log') IS NULL EXEC(N'CREATE SCHEMA [log];');

GO

ALTER SCHEMA [log] TRANSFER [RequestLog];

GO

CREATE TABLE [log].[AppLog] (
    [AppLogId] int NOT NULL IDENTITY,
    [Message] nvarchar(max) NULL,
    [MessageTemplate] nvarchar(max) NULL,
    [Level] nvarchar(128) NULL,
    [TimeStamp] datetime NOT NULL,
    [Category] nvarchar(max) NULL,
    [Application] nvarchar(max) NULL,
    [Exception] nvarchar(max) NULL,
    [Properties] nvarchar(max) NULL,
    CONSTRAINT [PK_AppLog] PRIMARY KEY ([AppLogId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190322030538_AddSerilog', N'2.2.2-servicing-10034');

GO


