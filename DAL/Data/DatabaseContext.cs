using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    /// <summary>
    /// [DatabaseContext] erstellt eine Verbindung zwischen Web-API und Datenbank
    /// [DbContext] primäre Klasse für die Interaktion mit der Datenbank
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlite(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }
            public DbContextOptions<DatabaseContext> dbOptions { get; set; }

            private AppConfiguration settings { get; set; }

        }

        public static OptionsBuild ops = new OptionsBuild();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Es enthält alle erforderlichen
        /// datenbankbezogenen Konfigurationsinformationen,
        /// z. B. Datenbankanbieter (SQLite) </param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        /// <summary>
        /// Ein DbSet repräsentiert die Collection aller Benutzer Entitäten
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
