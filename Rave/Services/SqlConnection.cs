using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;

namespace Rave.Services
{
    public class SqlConnection
    {
        public MySqlConnection Connection { get; set; }
        public SqlConnection()
        {
            string connectionString =
                    "server=localhost;" +
                    "database=rave;" +
                    "uid=root;" +
                    "pwd='';";
            Connection = new MySqlConnection(connectionString);
        }

        public void InsertValueIntoTable(string tableName, string columnName, string columnValue)
        {
            // Abrir la conexión a la base de datos
            Connection.Open();

            // Crear un objeto que representa la consulta SQL de inserción
            string insertQuery = "INSERT INTO " + tableName + "(" + columnName + ") VALUES(@value)";

            // Crear un objeto que representa el comando SQL
            MySqlCommand command = new MySqlCommand(insertQuery, Connection);

            // Asignar el valor a insertar
            command.Parameters.AddWithValue("@value", columnValue);

            // Ejecutar el comando SQL
            command.ExecuteNonQuery();

            // Cerrar la conexión a la base de datos
            Connection.Close();
        }

        public void UpdateValueIntoTable(string tableName, string columnName, string columnValue, int rowId)
        {
            // Abrir la conexión a la base de datos
            Connection.Open();

            // Crear un objeto que representa la consulta SQL de inserción
            string insertQuery = "UPDATE  " + tableName + " SET " + columnName + " = @value, UpdateDate = @updateDate WHERE Id = @rowid";

            // Crear un objeto que representa el comando SQL
            MySqlCommand command = new MySqlCommand(insertQuery, Connection);

            // Asignar el valor a insertar
            command.Parameters.AddWithValue("@value", columnValue);
            command.Parameters.AddWithValue("@rowid", rowId);
            command.Parameters.AddWithValue("@updateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            // Ejecutar el comando SQL
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void UpdateProfileImage(byte[] bImage, int rowId)
        {
            // Abrir la conexión a la base de datos
            Connection.Open();
            // Crear un objeto que representa la consulta SQL de inserción
            string insertQuery = "UPDATE  userinfo SET ProfileImage = @value WHERE Id = @rowid";
            // Crear un objeto que representa el comando SQL
            MySqlCommand command = new MySqlCommand(insertQuery, Connection);
            // Asignar el valor a insertar
            command.Parameters.AddWithValue("@value", bImage);
            command.Parameters.AddWithValue("@rowid", rowId);
            // Ejecutar el comando SQL
            command.ExecuteNonQuery();
            // Cerrar la conexión a la base de datos
            Connection.Close();
        }

        // Ejecutar una consulta SQL
        public void ExecuteQuery(MySqlCommand insertQuery)
        {
            Connection.Open();

            insertQuery.Connection = Connection;
            insertQuery.ExecuteNonQuery();

            Connection.Close() ;
        }

        // Ejecutar una consulta de selección SQL
        public List<string> SelectFrom(string selectQuery)
        {
            Connection.Open();
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, Connection);
            MySqlDataReader reader = selectCommand.ExecuteReader();
            List<string> result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetInt32(0).ToString());
            }
            Connection.Close();
            return result;
        }

        public ImageSource SelectImage(string selectQuery)
        {
            Connection.Open();
            MySqlCommand selectCommand = new MySqlCommand(selectQuery, Connection);
            MySqlDataReader reader = selectCommand.ExecuteReader();
            ImageSource result = null;
            while (reader.Read())
            {
                result = clsImagen.ConvertByteArrayToImage((byte[])reader["Image"]);
            }
            Connection.Close();
            return result;
        }
    }
}
