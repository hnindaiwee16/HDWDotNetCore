﻿using HDWDotNetCore.RestAPI;
using HDWDotNetCoreRestAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace HDWDotNetCoreRestAPI.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.stringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
