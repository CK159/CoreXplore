--Manual data migration
UPDATE log.AppLog
SET level = '2'
WHERE level = 'Information'

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[log].[AppLog]') AND [c].[name] = N'Level');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [log].[AppLog] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [log].[AppLog] ALTER COLUMN [Level] tinyint NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190324033617_LogLevelTinyint', N'2.2.2-servicing-10034');

GO


