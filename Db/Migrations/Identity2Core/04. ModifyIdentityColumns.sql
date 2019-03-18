/*
This script will upgrade existing column sizes to new sizes (ASP.net Core Identity)

NOTE: Before run this query, please check foreign keys names are as same as in existing names.

*/
--Update Id column of AspNetUsers table and related foreign key columns.

-- Drop foreign key constraints first.
ALTER TABLE [dbo].AspNetUserLogins DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].AspNetUserClaims DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

--Alter column size
ALTER TABLE [dbo].AspNetUsers ALTER COLUMN Id nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE [dbo].AspNetUserLogins ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE [dbo].AspNetUserClaims ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE [dbo].AspNetUserRoles ALTER COLUMN UserId nvarchar(450) NOT NULL;
GO

--Create foreign key constraints again.
ALTER TABLE [dbo].AspNetUserLogins     
ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES [dbo].AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

ALTER TABLE [dbo].AspNetUserClaims     
ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES [dbo].AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

ALTER TABLE [dbo].AspNetUserRoles     
ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId)     
    REFERENCES [dbo].AspNetUsers (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 

-------------------------------------------------------------------------------------------

--Update Id column of AspNetRoles table and related foreign key columns.

-- Drop foreign key constraints first.
ALTER TABLE [dbo].AspNetUserRoles DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

--Alter column size
ALTER TABLE [dbo].AspNetRoles ALTER COLUMN Id nvarchar(450) NOT NULL;
GO

--Alter column size
ALTER TABLE [dbo].AspNetUserRoles ALTER COLUMN RoleId nvarchar(450) NOT NULL;
GO

--Create foreign key constraints again.
ALTER TABLE [dbo].AspNetUserRoles     
ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY (RoleId)     
    REFERENCES [dbo].AspNetRoles (Id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE   
GO 
