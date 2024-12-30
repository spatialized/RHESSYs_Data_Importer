using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using RHESSYs_Data_Importer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHESSYs_Data_Importer.DAL
{
    /// <summary>
    /// The fire data database context.
    /// </summary>
    public class FireDataDbContext : DbContext
    {
        //private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=EFCore;Trusted_Connection=True;";

        public FireDataDbContext()
        {
        }

        public FireDataDbContext(DbContextOptions<FireDataDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if USE_MYSQL
            string connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["FireDataContext"].ConnectionString;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
#else
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CubeDataContext"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
#endif
        }

        public DbSet<FireDataFrameJSONRecord> FireData { get; set; }
    }

}
