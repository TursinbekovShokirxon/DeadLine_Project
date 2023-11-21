using Domain.Models;
using Domain.Models.Authtification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration _config) : base(options) {
            this._config = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString("ShokirsDatabase"));
        }
        public DbSet<Domain.Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserAuth> TaskStatuses { get; set; }
        public DbSet<TaskStatus> UserAuthifications { get; set; }
    }
}
