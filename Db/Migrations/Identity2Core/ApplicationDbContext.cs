using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cft.Fbo.AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace Cft.Fbo.AuthService.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
      //builder.Entity<UserRolePermission>().ToTable("AspNetUserRolePermissions");
      builder.Entity<IdentityRole>().ToTable("AspNetRoles");
      builder.Entity<ApplicationRole>().ToTable("AspNetRoles");
    }
  }
}
