using Microsoft.Extensions.Configuration;

namespace DAL.Data
{
    /// <summary>
    /// Abrufen der ConnectionStrings aus der Datei appsettings.Json
    /// </summary>
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnection");
            sqlConnectionString = appSetting.Value;
        }

        public string sqlConnectionString { get; set; }
    }
}
