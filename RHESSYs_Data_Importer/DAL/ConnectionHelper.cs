using System;
using System.IO;
using System.Text.Json;

namespace RHESSYs_Data_Importer.DAL
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            const string configPath = "appsettings.Development.json";
            if (File.Exists(configPath))
            {
                try
                {
                    var json = File.ReadAllText(configPath);
                    using var doc = JsonDocument.Parse(json);
                    return doc.RootElement
                        .GetProperty("Database")
                        .GetProperty("ConnectionString")
                        .GetString();
                }
                catch (Exception)
                {
                    // If there's any error reading the file, fall back to the default
                }
            }

            // Fallback (safe placeholder, not real credentials)
            return "server=localhost;port=3306;database=bigcreek_rhessys;user=root;password=;charset=utf8mb4;";
        }
    }
}
