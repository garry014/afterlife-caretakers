using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class ALCDBContext: DbContext
    {
        private readonly IConfiguration _config;
        public ALCDBContext(IConfiguration configuration)
        {
            _config = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get connection string from the value of "MyConn" in appsettings and  
            // configure context to connect to microsoft sql server database
            string connectionString = _config.GetConnectionString("MyConn");
            optionsBuilder.UseSqlServer(connectionString);
        }
        // Map Employee entity to Employees table in databse
        //public DbSet<Employee> Employees { get; set; }
    }
}
