using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class AppDbContext:DbContext
    {
        private readonly IConfiguration _config;
        public AppDbContext(IConfiguration _config)
        {
            this._config = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("ShokirsDatabase"));
        }
        public DbSet<Domain.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
