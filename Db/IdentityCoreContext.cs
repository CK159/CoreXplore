using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class IdentityCoreContext : IdentityDbContext
    {
        public IdentityCoreContext(DbContextOptions<IdentityCoreContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("idCore");
        }
    }
}