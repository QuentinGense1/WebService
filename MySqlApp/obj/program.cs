using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Build configuration
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        IConfiguration config = builder.Build();

        // Get the connection string
        string connectionString = config.GetConnectionString("DefaultConnection");

        // Create a connection to the database
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                // Open the connection
                connection.Open();
                Console.WriteLine("Connection to database successful!");

                // Perform database operations
                // For example, read data from the "client" table
                string sql = "SELECT Nom FROM client";
                using (var command = new MySqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Nom"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
