namespace IdentityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idFrameworkInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "idFramework.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "idFramework.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("idFramework.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("idFramework.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "idFramework.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "idFramework.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("idFramework.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "idFramework.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("idFramework.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("idFramework.AspNetUserRoles", "UserId", "idFramework.AspNetUsers");
            DropForeignKey("idFramework.AspNetUserLogins", "UserId", "idFramework.AspNetUsers");
            DropForeignKey("idFramework.AspNetUserClaims", "UserId", "idFramework.AspNetUsers");
            DropForeignKey("idFramework.AspNetUserRoles", "RoleId", "idFramework.AspNetRoles");
            DropIndex("idFramework.AspNetUserLogins", new[] { "UserId" });
            DropIndex("idFramework.AspNetUserClaims", new[] { "UserId" });
            DropIndex("idFramework.AspNetUsers", "UserNameIndex");
            DropIndex("idFramework.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("idFramework.AspNetUserRoles", new[] { "UserId" });
            DropIndex("idFramework.AspNetRoles", "RoleNameIndex");
            DropTable("idFramework.AspNetUserLogins");
            DropTable("idFramework.AspNetUserClaims");
            DropTable("idFramework.AspNetUsers");
            DropTable("idFramework.AspNetUserRoles");
            DropTable("idFramework.AspNetRoles");
        }
    }
}
