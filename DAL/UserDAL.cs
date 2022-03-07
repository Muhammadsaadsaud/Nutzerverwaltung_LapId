using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// DAL: Data Access Layer.
    /// Es bietet einen vereinfachten Zugriff auf die Datenbank
    /// </summary>
    public class UserDAL : IUserInterface
    {
        // Erstellt einen neuen Benutzer und aktualisiert die Datenbank
        public async Task<User> CreateUser(User user)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }
                return user;
            }
            catch (Exception)
            {

                return null;
            }
        }

        // Löscht einen neuen Benutzer und aktualisiert die Datenbank
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    var user = await context.Users.FindAsync(id);
                    // ob ein User vorhanden ist
                    if (user == null)
                    {
                        return false;
                    }
                    // Nutzer aus der Datenbank löschen
                    context.Users.Remove(user);
                    // Änderungen asynchron in der Datenbank speichern
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Gibt einen bestimmten Benutzer aus der Datenbank
        public async Task<User> GetUser(int id)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    var user = await context.Users.FindAsync(id);
                    return user;

                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        // Bietet alle Benutzer aus der Datenbank
        public async Task<List<User>> GetUsers()
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    return await context.Users.ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Aktualisiert einen bestimmten Benutzer in der Datenbank
        public async Task<bool> UpdateUser(User newUser)
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    var user = await context.Users.FindAsync(newUser.Id);
                    user.Firstname = newUser.Firstname;
                    user.Lastname = newUser.Lastname;
                    user.Login = newUser.Login;
                    user.CreationDate = newUser.CreationDate;
                    user.ChangeDate = newUser.ChangeDate;
                    user.PasswordHash = newUser.PasswordHash;
                    user.PasswordSalt = newUser.PasswordSalt;
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        /// <summary>
        /// Eine public Methode, um zu überprüfen,
        /// ob ein Benutzer in einer Datenbank vorhanden ist
        /// </summary>
        /// <param name="login"></param>
        /// <returns>bool(true/false)</returns>
        public async Task<bool> UserExists(string login)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                return await context.Users.AnyAsync(x => x.Login == login.ToLower());
            }

        }

        /// <summary>
        /// Eine public Methode, um zu überprüfen,
        /// ob ein Login des Benutzers in einer Datenbank vorhanden ist
        /// </summary>
        /// <param name="login"></param>
        /// <returns>bool(User)</returns>
        public async Task<User> LoginExists(string login)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                return await context.Users.SingleOrDefaultAsync(x => x.Login == login.ToLower());
            }

        }

        /// <summary>
        /// Eine public Methode, um zu überprüfen,
        /// ob ein ein Benutzer mit angegebener in einer Datenbank vorhanden ist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>(User)</returns>
        public async Task<User> FindById(int id)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                return await context.Users.FindAsync(id);
            }
        }
    }
}
