using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Pract2.Models
{
    public class HRDbContext: DbContext
    {
        // Inject Iconfiguration to access appsettings.json
         private readonly IConfiguration _config;
        public HRDbContext(IConfiguration configuration ) 
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
        public DbSet<Employee> Employees { get; set; }
         
    }
}
