using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace RealEstateDatabaseApp
{
    public static class Database
    {
        private static string connectionString = "Server=127.0.0.1;Database=wynajem_nieruchomosci;Uid=root;Pwd=projekt123;";

        // Metoda do wykonywania zapytań SELECT
        public static DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            DataTable table = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }

        // Metoda do wykonywania zapytań typu INSERT, UPDATE, DELETE
        public static int ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery(); // Zwraca liczbę zmodyfikowanych wierszy
                }
            }
        }
        
        public static DataTable ExecuteQuery(string query)
        {
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }

        }


        
    }
}