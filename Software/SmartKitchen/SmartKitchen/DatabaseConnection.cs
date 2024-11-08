using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace SmartKitchen
{
    public class DatabaseConnection : IDisposable
    {
        private MySqlConnection _connection;

        public DatabaseConnection()
        {
            string connectionString = "Server=192.168.157.47;Port=3306;Database=mydatabase;User ID=daapje;Password=password;";
            //string connectionString = "Server=192.168.157.41;Port=3306;Database=smartkitchen;User ID=applicationacc;Password=SuperSecureHyperGood#1999;";
            _connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();

                if(_connection.State == System.Data.ConnectionState.Open)
                {
                    //MessageBox.Show("Succesfull!");
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
        public (List<string> result, List<int> hoeveelheid) ExecuteQuery(string query, string kolomnaam1, string kolomnaam2)
        {
            List<string> result = new List<string>();
            List<int> hoeveelheid = new List<int>();
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.IsDBNull(reader.GetOrdinal(kolomnaam1)) ? "" : reader[kolomnaam1].ToString());
                        hoeveelheid.Add(reader.IsDBNull(reader.GetOrdinal(kolomnaam2)) ? 0 : Convert.ToInt32(reader[kolomnaam2]));

                    }
                }
            }
            
            catch (MySqlException ex)
            {
                MessageBox.Show($"Fout bij het uitvoeren van de query: {ex.Message}");
            }
            
            finally
            {
                CloseConnection();
            }
            return (result, hoeveelheid);
        }

        public void UpdateDatabase(string naam, int getal)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE medewerkers SET hoeveelheid = @getal WHERE naam = @naam";
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@getal", getal);
                cmd.Parameters.AddWithValue("@naam", naam);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
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
