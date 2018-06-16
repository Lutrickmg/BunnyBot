using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;



namespace BunnyBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            Options.UseSqlite($"Data Source=Database.db");
        }
    }
}
