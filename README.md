# CoreXplore
.NET Core feature exploration

**Features**

* .Net Core 2.2
* Entity Framework Core 2.2
* Core First Migrations
* AdminLTE theme
* Microsoft Library Manager tool - Libman
* Dependency Injection
* Environment-specific config
* Azure deployable
* Database tables automatically non-pluralized (default convention is to be pluralized)
* Logging middleware which records details on all server requests/responses
* ASP.NET Core Identity
* .editorconfig file
* MvcPages - Conveniently colocate controller, views, models, css, js files together in single folder
* GenericServices
* Compatibility between Identity non-core and Identity core (use same account in both frameworks)
* .Net Core application versioning
* Serilog with MSSQL and Email logging

# General Info

**Handle multiple contexts**

If the project has multiple DbContexts in it, you will need to specify the one you want to use for each operation you perform. All commands below specify the context explicitly as this project has DbCore and IdentityCoreContext in it.

**Create a new migration**
* Open terminal from Db project!
* dotnet ef migrations add `Migration_Name` --context DbCore --startup-project "../App"
    
**Update database - Apply migrations**
* Open terminal from Db project!
* dotnet ef database update --context DbCore --startup-project "../App"
    
**Generate migration script from some base migration to latest migration**
* Open terminal from Db project!
* dotnet ef migrations script `FROM_MIGRATION_ID` `TO_MIGRATION_ID` --context DbCore --startup-project "../App" > migrate.sql
* `*_MIGRATION_ID` fields are the full migration names including date stamp such as: 20181112043643_Initial
* `TO_MIGRATION_ID` is optional
* Find migrate.sql and review changes
* Save sql script in MigrationSQL folder with name of: `Date_MigrationName.sql`
* Run script on database

**Remove migration**
* Open terminal from Db project!
* dotnet ef migrations remove --context DbCore --startup-project "../App"

**Add & Scaffold Identity**
* Add Microsoft.VisualStudio.Web.CodeGeneration.Design package to web app project
* Add Microsoft.AspNetCore.Identity.EntityFrameworkCore to Db project
* Add Microsoft.Extensions.Identity.Stores to Db project
* Create DbContext class that inherits from IdentityDbContext
* Change default schema if desired
* dotnet aspnet-codegenerator identity -dc Db.IdentityCoreContext --files "Account.Register;Account.Login;Account.Logout"
* dotnet ef migrations add InitialIdentity --context IdentityCoreContext --startup-project "../App"

**Compatibility between Identity non-core and Identity Core**
* Goals
  * Allow existing Identity non-core data to be used in Identity Core
  * Allow users to log into both non-core and core applications, regardless of where account was created
* Database migration scripts included in the Db/Migrations/Identity2Core
  * Based on https://github.com/aspnet/Docs/issues/6425#issuecomment-442180056
  * Modified to exclude non-standard AspNetUserRolePermissions 
  * Modified to include schema prefixes on all operations (easier to find & replace if using non-standard schema)
  * Add script to update existing user records with new required data
* `LockoutEndDateUtc` renamed to `LockoutEnd` in AspNetUsers
  * Remap non-core `LockoutEndDateUtc` field to `LockoutEnd` column
* Identity Core uses an incompatible password hashing algorithm and auto-upgrades saved password hashes on successful login
  * Re-enable old V2 hashing algorithm with `services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);` in ConfigureServices() of core application
* Usage of `NormalizedEmail` and `NormalizedUserName` in AspNetUsers
  * Identity Core will not allow login without these columns being populated
* Handle misc field additions and changes via Microsoft.AspNet.Identity.AspNetCoreCompat applied to non-core project
  * https://stackoverflow.com/a/53578369
