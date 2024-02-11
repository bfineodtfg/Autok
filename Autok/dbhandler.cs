using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    public class dbHandler
    {
        private string serverAddress;
        private string dbName;
        private string username;
        private string password;
        static private string connectionString;
        MySqlConnection connection;
        public dbHandler()
        {
            serverAddress = "127.1.1.1";
            dbName = "hazi";
            username = "user";
            password = "pass";
            connectionString = $"Server={serverAddress};Database={dbName};User={username};Password={password};";
            connection = new MySqlConnection(connectionString);
        }

        public void ReadDb()
        {
            Autok.all.Clear();
            try
            {
                connection.Open();
                string query = "SELECT * FROM auto";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    new Autok(reader);
                }
                reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show($"Error: {e.Message}");
            }
        }
        public bool InsertCar(Autok car)
        {
            try
            {
                connection.Open();
                string query = $"INSERT INTO `auto` (`rendszam`, `marka`, `modell`, `gyartasiev`, `forgalmiErvenyesseg`, `vetelar`, `kmallas`, `hengerurtartalom`, `tomeg`, `teljesitmeny`) VALUES ('{car.Rendszam}', '{car.Make}' , '{car.Model}', '{car.Year}', '{car.Registration}', '{car.Price}', '{car.KM}', '{car.Displacement}', '{car.Weight}', '{car.Power}'  );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                Autok.Error(car);
                MessageBox.Show($"Error: {e.Message}");
                return false;
            }
        }
        public bool updateCar(Autok car, string previousRendszam)
        {
            try
            {
                connection.Open();
                string query = $"update `auto` SET rendszam='{car.Rendszam}', marka='{car.Make}' , modell='{car.Model}', gyartasiev='{car.Year}', forgalmiErvenyesseg='{car.Registration}', vetelar='{car.Price}', kmallas='{car.KM}', hengerűrtartalom='{car.Displacement}', tomeg='{car.Weight}', teljesitmeny='{car.Power}' where rendszam = '{previousRendszam}';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                Autok.Error(car);
                MessageBox.Show($"Error: {e.Message}");
                return false;
            }
        }
        public bool deleteCar(string rendszam)
        {
            try
            {
                connection.Open();
                string query = $"delete from `auto` where rendszam = \"{rendszam}\";";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                //Tagok.all.RemoveAt(id);
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show($"Error: {e.Message}");
                return false;
            }
        }
    }
}