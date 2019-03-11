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
* dotnet ef migrations script `MIGRATION_ID` --context DbCore --startup-project "../App" > migrate.sql
* `MIGRATION_ID` is the full migration name including date stamp such as: 20181112043643_Initial
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
* `LockoutEndDateUtc` renamed to `LockoutEnd` in AspNetUsers
  * This probably means that both columns need to be present
* Identity Core uses an incompatible password hashing algorithm and auto-upgrades saved password hashes on successful login
  * Re-enable old V2 hashing algorithm with `services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);` in ConfigureServices()
* Usage of `NormalizedEmail` and `NormalizedUserName` in AspNetUsers
  * Identity Core will not allow login without these columns being populated
