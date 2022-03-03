using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// [DataContext] erstellt eine Verbindung zwischen Web-API und Datenbank
    /// [DbContext] primäre Klasse für die Interaktion mit der Datenbank
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Es enthält alle erforderlichen
        /// datenbankbezogenen Konfigurationsinformationen,
        /// z. B. Datenbankanbieter (SQLite) </param>
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Ein DbSet repräsentiert die Collection aller Benutzer Entitäten
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
