# CoreXplore
.NET Core feature exploration

**Features**

* .Net Core 2.1
* Entity Framework Core 2.1
* Core First Migrations
* AdminLTE theme
* Microsoft Library Manager tool - Libman
* Dependency Injection
* Environment-specific config
* Azure deployable
* Database tables automatically non-pluralized (default convention is to be pluralized)
* Logging middleware which records details on all server requests/responses

# General Info

**Handle multiple contexts**

If the project has multiple DbContexts in it, you will need to specify the one you want to use for each operation you perform. All commands below specify the context explicitly as this project has Dbc and IdentityCoreContext in it.

**Create a new migration**
* Open terminal from Db project!
* dotnet ef migrations add _Migration_Name_ --context Dbc --startup-project "../App"
    
**Update database - Apply migrations**
* Open terminal from Db project!
* dotnet ef database update --context Dbc --startup-project "../App"
    
**Generate migration script from some base migration to latest migration**
* Open terminal from Db project!
* dotnet ef migrations script _MIGRATION_ID_ --context Dbc --startup-project "../App" > migrate.sql
* _MIGRATION_ID_ is the full migration name including date stamp such as: 20181112043643_Initial
* Find migrate.sql and review changes
* Save sql script in MigrationSQL folder with name of: _Date_MigrationName_.sql
* Run script on database

**Remove migration**
* Open terminal from Db project!
* dotnet ef migrations remove --context Dbc --startup-project "../App"

**Add & Scaffold Identity**
* Add Microsoft.VisualStudio.Web.CodeGeneration.Design package to web app project
* Add Microsoft.AspNetCore.Identity.EntityFrameworkCore to Db project
* Add Microsoft.Extensions.Identity.Stores to Db project
* Create DbContext class that inherits from IdentityDbContext
* Change default schema if desired
* dotnet aspnet-codegenerator identity -dc Db.IdentityCoreContext --files "Account.Register;Account.Login;Account.Logout"
* dotnet ef migrations add InitialIdentity --context IdentityCoreContext --startup-project "../App"