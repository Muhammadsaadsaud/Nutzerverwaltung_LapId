using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Entities;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    /// <summary>
    /// BLL: Bussiness Logic Layer
    /// Diese Ebene trennt die DAL (Data Access Layer) davon,
    /// ihre Entitäten Controllern zugänglich zu machen.
    /// 
    /// BLL verwaltet die Kommunikation zwischen einer Endbenutzerschnittstelle und einer Datenbank.
    /// </summary>
    public class UserBLL
    {
        // DAL 
        private DAL.UserDAL _DAL;
        // Automapper
        private Mapper userMapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserBLL()
        {
            // Erstellt einer Instanz von DAL
            _DAL = new DAL.UserDAL();
            var configUserMapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>().ReverseMap());
            userMapper = new Mapper(configUserMapper);
        }

        /// <summary>
        /// Eine asynchrone Methode zum Erstellen eines Benutzers
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>UserDto</returns>
        public async Task<UserDto> CreateUser(RegisterDto registerDto)
        {
            try
            {
                if (await _DAL.UserExists(registerDto.Login))
                {
                    // gib false zurück, wenn Login existiert
                    return null;
                }
                // eine Instanz von HMACSHA512 erstellt
                using var hmac = new HMACSHA512();

                var user = new User();
                user.Firstname = registerDto.Firstname.ToLower();
                user.Lastname = registerDto.Lastname.ToLower();
                user.Login = registerDto.Login.ToLower();
                user.CreationDate = DateTime.Now;
                user.ChangeDate = DateTime.Now;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
                user.PasswordSalt = hmac.Key;

                var userEntity = await _DAL.CreateUser(user);
                UserDto userDto = userMapper.Map<User, UserDto>(userEntity);
                return userDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Eine asynchrone Methode, um sich als Benutzer anzumelden
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>UserDto</returns>
        public async Task<UserDto> LoginUser(LoginDto loginDto)
        {
            try
            {
                var userEntity = await _DAL.LoginExists(loginDto.Login);
                var userDto = userMapper.Map<User, UserDto>(userEntity);
                // ob ein Loign vorhanden ist
                if (userEntity == null)
                {
                    // gib false zurück, wenn Login nicht existiert
                    return null;
                }
                // Erstellte eine Instanz von HMACSHA512 mit PasswordSalt
                using var hmac = new HMACSHA512(userEntity.PasswordSalt);
                // Berechnet das gehashte Passwort für das vom Benutzer bereitgestellte Passwort
                var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                // Prüft jedes Byte des HashPassword
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != userEntity.PasswordHash[i]) return null;
                }

                return userDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Eine asynchrone Methode zum Löschen eines Benutzers
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return await _DAL.DeleteUser(id);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// Eine asynchrone Methode zum Aktualisieren eines Benutzers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserDto"></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                var userEntity = await _DAL.FindById(id);

                // ob ein User vorhanden ist
                if (userEntity == null)
                {
                    // gib false zurück, wenn User nicht existiert
                    return false;
                }
                else if (await _DAL.UserExists(updateUserDto.Login))
                {
                    // gib false zurück, wenn Login bereit existiert
                    return false;
                }

                using var hmac = new HMACSHA512();

                userEntity.Firstname = updateUserDto.Firstname.ToLower();
                userEntity.Lastname = updateUserDto.Lastname.ToLower();
                userEntity.Login = updateUserDto.Login.ToLower();
                userEntity.ChangeDate = DateTime.Now;
                if (!string.IsNullOrEmpty(updateUserDto.Password))
                {
                    userEntity.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUserDto.Password));
                    userEntity.PasswordSalt = hmac.Key;
                }

                return await _DAL.UpdateUser(userEntity);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// Eine asynchrone Methode, um alle Benutzer zurückzugeben als List<UserDto>
        /// </summary>
        /// <returns><List<UserDto>></returns>
        public async Task<List<UserDto>> GetUsers()
        {
            List<User> userEntity = await _DAL.GetUsers();
            List<UserDto> userDto = userMapper.Map<List<User>, List<UserDto>>(userEntity);
            return userDto;
        }

        /// <summary>
        /// Eine asynchrone Methode, um eine Benutzer zurückzugeben
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDto</returns>
        public async Task<UserDto> GetUser(int id)
        {
            User userEntity = await _DAL.GetUser(id);
            UserDto userDto = userMapper.Map<User, UserDto>(userEntity);
            return userDto;
        }
    }
}
