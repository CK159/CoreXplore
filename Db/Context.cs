using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class Dbc : DbContext
    {
        /*public Dbc()
        {
        }*/
        
        public Dbc(DbContextOptions<Dbc> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string now configured through Startup.cs
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CoreXplore;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new RequestLogConfiguration());
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
    }
}