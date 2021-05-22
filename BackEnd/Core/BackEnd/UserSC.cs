using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Models;

namespace Core.BackEnd
{
    public class UserSC : BaseSC
    {
        // Regresa un IQueryable con todos los usuarios de la Base de Datos.
        private IQueryable<User> GetAllUsers()
        {
            return dbContext.Users;
        }

        // Devuelve true si el usuario existe en la Base de Datos, y false en caso contrario.
        public bool UserExists(string username)
        {
            User dbUser = GetAllUsers().FirstOrDefault(user => user.Username == username);

            return dbUser != null;
        }

        // Devuelve true si el correo existe en la Base de Datos, y false en caso contrario.
        public bool EmailExists(string email)
        {
            User dbUser = GetAllUsers().FirstOrDefault(user => user.Email == email);

            return dbUser != null;
        }

        // Recibe un usuario y una contraseña. Si la contraseña hasheada coincide con la contraseña almacenada
        // en la Base de Datos devuelve un Token JWT.
        public string Login(string username, string password)
        {
            // Busca al usuario en la Base de Datos.
            User dbUser = GetAllUsers().FirstOrDefault(user => user.Username == username);

            if (dbUser == null)
                throw new KeyNotFoundException();

            // Compara las contraseñas.
            bool passwordsMatch = CryptoSC.ComparePasswords(dbUser.Hashpassword, password, dbUser.Salt);

            // Si las contraseñas coinciden genera un Token JWT y lo devuelve.
            return passwordsMatch ? CryptoSC.GenerateToken(dbUser) : null;
        }

        // Da de alta a un usuario a la Base de Datos.
        public void SignUp(UserPostDTO newUser)
        {
            User dbUser = newUser.GetDataBaseObject();

            dbUser.Userid = Guid.NewGuid();
            dbUser.Salt = CryptoSC.GenerateSalt(); ;
            dbUser.Hashpassword = CryptoSC.GetHashedPassword(newUser.Password, dbUser.Salt);

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }
    }
}
