

/*
	AspNetUsers table changes
*/
GO
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers];
GO
EXEC sp_rename N'[dbo].AspNetUsers.LockoutEndDateUtc', N'LockoutEnd', N'COLUMN';
GO
ALTER TABLE [dbo].[AspNetUsers] ADD [ConcurrencyStamp] nvarchar(max) NULL;
GO
ALTER TABLE [dbo].[AspNetUsers] ADD [NormalizedEmail] nvarchar(256) NULL;
GO
ALTER TABLE [dbo].[AspNetUsers] ADD [NormalizedUserName] nvarchar(256) NULL;
GO
CREATE INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail]);
GO
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

/*
	AspNetRoles table changes
*/
GO
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles];
GO
ALTER TABLE [dbo].[AspNetRoles] ADD [ConcurrencyStamp] nvarchar(max) NULL;
GO
ALTER TABLE [dbo].[AspNetRoles] ADD [NormalizedName] nvarchar(256) NULL;
GO
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

/*
	AspNetUserClaims table changes
*/
GO
EXEC sp_rename N'[dbo].AspNetUserClaims.IX_UserId', N'IX_AspNetUserClaims_UserId', N'INDEX';
GO

/*
	AspNetUserLogins table changes
*/
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [PK_dbo.AspNetUserLogins];
GO
EXEC sp_rename N'[dbo].AspNetUserLogins.IX_UserId', N'IX_AspNetUserLogins_UserId', N'INDEX';
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD [ProviderDisplayName] nvarchar(max) NULL;
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]);
GO

/*
	AspNetUserRoles table changes
*/
GO
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles];
GO
EXEC sp_rename N'[dbo].AspNetUserRoles.IX_RoleId', N'IX_AspNetUserRoles_RoleId', N'INDEX';
GO


