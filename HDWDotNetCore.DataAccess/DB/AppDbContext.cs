using Microsoft.EntityFrameworkCore;
using HDWDotNetCore.NLayer.DataAccess.Models;


namespace HDWDotNetCore.NLayer.DataAccess.DB
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.stringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
