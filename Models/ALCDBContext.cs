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

        // Map entity to table in databse
        public DbSet<Casket> Caskets { get; set; }
        public DbSet<Funeral> FuneralPlans { get; set; }
        public DbSet<FExecutorPermission> FExecutorPermission { get; set; }
        public DbSet<Video> VideoMemo { get; set; }
        public DbSet<BVideoPermission> BVideoPermission { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Users> users { get; set; }
        public static object Users { get; internal set; }
        public DbSet<AMDWitness> amdwitness { get; set; }
        public DbSet<Admins> admins { get; set; }
        public static object Admin { get; internal set; }
        public DbSet<PersonalInformation> Personal { get; set; }
        public DbSet<BeneficiaryInformation> Beneficiary { get; set; }
        public DbSet<MaritalInfo> Fiance { get; set; }
        public DbSet<Gift> Asset { get; set; }
        public DbSet<ExecutorInformation> Executor { get; set; }
        public DbSet<WitnessInformation> Witness { get; set; }
        
    }
}
