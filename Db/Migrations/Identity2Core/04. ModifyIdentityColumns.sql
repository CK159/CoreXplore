/*
This script will upgrade existing column sizes to new sizes (ASP.net Core Identity)

NOTE: Before run this query, please check foreign keys names are as same as in existing names.

*/
--Update Id column of AspNetUsers table and related foreign key columns.

-- Drop foreign key constraints first.
ALTER TABLE AspNetUserLogins DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE AspNetUserClaims DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

--Alter column size
ALTER TABLE AspNetUsers ALTER COLUMN Id nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE AspNetUserLogins ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE AspNetUserClaims ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE AspNetUserRoles ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Create foreign key constraints again.
ALTER TABLE AspNetUserLogins     
ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

ALTER TABLE AspNetUserClaims     
ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

ALTER TABLE AspNetUserRoles     
ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

-------------------------------------------------------------------------------------------

--Update Id column of AspNetRoles table and related foreign key columns.

-- Drop foreign key constraints first.
ALTER TABLE AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

-- Drop foreign key constraints first.
ALTER TABLE AspNetUserRolePermissions DROP CONSTRAINT [FK_dbo.AspNetUserRolePermissions_dbo.AspNetRoles_RoleId]
GO

--Alter column size
ALTER TABLE AspNetRoles ALTER COLUMN Id nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE AspNetUserRoles ALTER COLUMN RoleId nvarchar(450) NOT NULL;
GO

DROP INDEX [IX_AspNetUserRolePermissions_RoleId] ON [dbo].[AspNetUserRolePermissions]
GO

--Alter column size
ALTER TABLE AspNetUserRolePermissions ALTER COLUMN RoleId nvarchar(450) NOT NULL;
GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserRolePermissions_RoleId] ON [dbo].[AspNetUserRolePermissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

--Create foreign key constraints again.
ALTER TABLE AspNetUserRoles     
ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY (RoleId)     
    REFERENCES AspNetRoles (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

--Create foreign key constraints again.
ALTER TABLE AspNetUserRolePermissions     
ADD CONSTRAINT [FK_dbo.AspNetUserRolePermissions_dbo.AspNetRoles_RoleId] FOREIGN KEY (RoleId)     
    REFERENCES AspNetRoles (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 