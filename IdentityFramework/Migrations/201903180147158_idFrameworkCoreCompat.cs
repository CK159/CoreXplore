namespace IdentityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idFrameworkCoreCompat : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "LockoutEndDateUtc", newName: "LockoutEnd");
            CreateTable(
                "dbo.AspNetRoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetRoles", "NormalizedName", c => c.String());
            AddColumn("dbo.AspNetRoles", "ConcurrencyStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "NormalizedUserName", c => c.String());
            AddColumn("dbo.AspNetUsers", "NormalizedEmail", c => c.String());
            AddColumn("dbo.AspNetUsers", "ConcurrencyStamp", c => c.String());
            AddColumn("dbo.AspNetUserLogins", "ProviderDisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUserLogins", "ProviderDisplayName");
            DropColumn("dbo.AspNetUsers", "ConcurrencyStamp");
            DropColumn("dbo.AspNetUsers", "NormalizedEmail");
            DropColumn("dbo.AspNetUsers", "NormalizedUserName");
            DropColumn("dbo.AspNetRoles", "ConcurrencyStamp");
            DropColumn("dbo.AspNetRoles", "NormalizedName");
            DropTable("dbo.AspNetRoleClaims");
            RenameColumn(table: "dbo.AspNetUsers", name: "LockoutEnd", newName: "LockoutEndDateUtc");
        }
    }
}
