using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rave;
using Rave.Models;
using SQLite;
using SQLitePCL;
using System.IO;

namespace Rave.Services
{
    public class SqLiteService
    {
        public string DbPathFavorites { get; set; }
        public string DbPathUserInfo { get; set; }
        SQLiteConnection ConnectionFavorites { get; set; }
        SQLiteConnection ConnectionUserInfo { get; set; }


        public SqLiteService()
        {
            // Ruta de la base de datos
            DbPathFavorites = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favorites.db3");
            DbPathUserInfo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserInfo.db3");
            // Crear la conexión
            ConnectionFavorites = new SQLiteConnection(DbPathFavorites);

        }

        // Crear la base de datos
        public void CreateDatabase()
        {
            // Crear la tabla de favoritos
            ConnectionFavorites.CreateTable<Favorite>();
        }

        

        // Eliminar la base de datos
        public void DropDatabase()
        {
            // Eliminar la base de datos si existe
            if (File.Exists(DbPathFavorites))
            {
                Console.WriteLine("The database has been droped");
                // Eliminar el archivo de la base de datos
                File.Delete(DbPathFavorites);
            }
            else
            {
                Console.WriteLine("The database file does not exist");
            }
        }

        // Seleccionar los datos de la tabla de favoritos
        public List<Favorite> GetFavorites()
        {
            List<Favorite> favorites = ConnectionFavorites.Table<Favorite>()
                .ToList();
            return favorites;
        }


        // Insertar un establecimineto en la tabla de favoritos
        public bool AddFavorite(Stablishment stablishment)
        {
            // Verificar si el favorito ya existe
            Favorite favorite = new Favorite
            {
                StablishmentId = stablishment.Id,
                UserId = (App.Current.Properties["user"] as User).Id
            };
            if (!ConnectionFavorites.Table<Favorite>().Any(f => f.StablishmentId == stablishment.Id))
            {
                // Insertar el favorito
                ConnectionFavorites.Insert(favorite
                    );
                return true;
            }
            return false;
        }

        // Eliminar un establecimiento de la tabla de favoritos
        public bool DelFavorite(Stablishment stablishment)
        {
            // Verificar si el favorito existe
            Favorite favorite = ConnectionFavorites.Table<Favorite>().FirstOrDefault(f => f.Id == stablishment.Id);

            // Eliminar el favorito
            if (favorite != null)
            {
                ConnectionFavorites.Delete(favorite);
                return true;
            }
            return false;
        }

        // Verificar si la base de datos existe y crearla si no existe
        public bool CreateDatabaseIfNotExists()
        {
            bool isCreated = false;

            // Ruta de la base de datos
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favorites.db3");

            // Si la base de datos no existe, crearla
            if (!File.Exists(dbPath))
            {
                // Crear la conexión
                CreateDatabase();
                isCreated = true;
            }
            // Retornar si la base de datos fue creada
            return isCreated;
        }

        public void CreateUserInfoDatabase()
        {
            // Crear la tabla de favoritos
            ConnectionUserInfo.CreateTable<User>();
        }

        public void DeleteUserInfoDatabase()
        {
            // Eliminar la base de datos si existe
            if (File.Exists(DbPathFavorites))
            {
                Console.WriteLine("The database has been droped");
                // Eliminar el archivo de la base de datos
                File.Delete(DbPathFavorites);
            }
            else
            {
                Console.WriteLine("The database file does not exist");
            }
        }

        public bool AddUser(User user)
        {
            // Verificar si el favorito ya existe
            if (!ConnectionUserInfo.Table<User>().Any(f => f.Id == user.Id))
            {
                // Insertar el favorito
                ConnectionUserInfo.Insert(user);
                return true;
            }
            return false;
        }

        public bool DelUser(User user)
        {
            // Verificar si el favorito existe
            User favorite = ConnectionUserInfo.Table<User>().FirstOrDefault(f => f.Id == user.Id);
            // Eliminar el favorito
            if (favorite != null)
            {
                ConnectionUserInfo.Delete(user);
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            // Verificar si el favorito existe
            User favorite = ConnectionUserInfo.Table<User>().FirstOrDefault(f => f.Id == user.Id);
            // Eliminar el favorito
            if (favorite != null)
            {
                ConnectionUserInfo.Update(user);
                return true;
            }
            return false;
        }
    }
}