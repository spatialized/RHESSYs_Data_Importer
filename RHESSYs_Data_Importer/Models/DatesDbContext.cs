using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHESSYs_Data_Importer.Models

{

    /// <summary>
    /// The cube data database context.
    /// </summary>
    public class DatesDbContext : DbContext
    {
        public DatesDbContext()
        {
        }

        public DatesDbContext(DbContextOptions<CubeDataDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if USE_MYSQL
            string connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["CubeDataContextVultr"].ConnectionString;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
#else
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CubeDataContext"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
#endif
        }

        public DbSet<Date> Dates { get; set; }
    }

}
