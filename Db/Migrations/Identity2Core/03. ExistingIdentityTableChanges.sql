

/*
	AspNetUsers table changes
*/
GO
DROP INDEX [UserNameIndex] ON [AspNetUsers];
GO
EXEC sp_rename N'AspNetUsers.LockoutEndDateUtc', N'LockoutEnd', N'COLUMN';
GO
ALTER TABLE [AspNetUsers] ADD [ConcurrencyStamp] nvarchar(max) NULL;
GO
ALTER TABLE [AspNetUsers] ADD [NormalizedEmail] nvarchar(256) NULL;
GO
ALTER TABLE [AspNetUsers] ADD [NormalizedUserName] nvarchar(256) NULL;
GO
CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO
CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

/*
	AspNetRoles table changes
*/
GO
DROP INDEX [RoleNameIndex] ON [AspNetRoles];
GO
ALTER TABLE [AspNetRoles] ADD [ConcurrencyStamp] nvarchar(max) NULL;
GO
ALTER TABLE [AspNetRoles] ADD [NormalizedName] nvarchar(256) NULL;
GO
CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

/*
	AspNetUserClaims table changes
*/
GO
EXEC sp_rename N'AspNetUserClaims.IX_UserId', N'IX_AspNetUserClaims_UserId', N'INDEX';
GO

/*
	AspNetUserLogins table changes
*/
GO
ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [PK_dbo.AspNetUserLogins];
GO
EXEC sp_rename N'AspNetUserLogins.IX_UserId', N'IX_AspNetUserLogins_UserId', N'INDEX';
GO
ALTER TABLE [AspNetUserLogins] ADD [ProviderDisplayName] nvarchar(max) NULL;
GO
ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]);
GO

/*
	AspNetUserRoles table changes
*/
GO
DROP INDEX [IX_UserId] ON [AspNetUserRoles];
GO
EXEC sp_rename N'AspNetUserRoles.IX_RoleId', N'IX_AspNetUserRoles_RoleId', N'INDEX';
GO


