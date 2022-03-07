using DAL.Entities;

namespace DAL
{
    /// <summary>
    /// Eine Schnittstelle zum Bearbeiten der Benutzertabelle
    /// </summary>
    public interface IUserInterface
    {
        Task<User> CreateUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(int id);

        Task<List<User>> GetUsers();

        Task<User> GetUser(int id);

    }
}
