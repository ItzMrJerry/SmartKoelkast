using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SmartKitchen
{
    public class DatabaseConnection : IDisposable
    {
        private MySqlConnection _connection;

        public DatabaseConnection()
        {
            string connectionString = "Server=192.168.157.41;Port=3306;Database=smartkitchen;User ID=applicationacc;Password=SuperSecureHyperGood#1999;";
            _connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();

                if(_connection.State == System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("Succesfull!");
                }
            }
        }

        public void CloseConnection()
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public void ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["medewerkers"]}");
                    }
                }
            }

            catch (MySqlException ex)
            {
                //Console.WriteLine($"Fout bij het uitvoeren van de query: {ex.Message}");
                MessageBox.Show($"Fout bij het uitvoeren van de query: {ex.Message}");
            }

            finally
            {
                CloseConnection();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
