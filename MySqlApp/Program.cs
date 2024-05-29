using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Configuration du build pour la connection BDD
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        IConfiguration config = builder.Build();

        // Récupération du string permettant la connection
        string connectionString = config.GetConnectionString("DefaultConnection");

        // Création de la connexion BDD
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                // Ouverture de la connection à la base de donnée
                connection.Open();
                Console.WriteLine("Connection to database successful!");

                // On fait quelques opérations sur la base de donnée
                // Lecture de la table client sur la colonne Nom
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
