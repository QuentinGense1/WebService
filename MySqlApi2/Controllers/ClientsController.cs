using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    //Déclaration de la configuration en lecture seul et privé
    private readonly IConfiguration _configuration;

    public ClientsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        var clients = new List<string>();
        //Récupération de la connection BDD
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //Connexion à la BDD
        using (var connection = new MySqlConnection(connectionString))
        {
            //On ouvre la connection afin de demander la liste des nom des clients
            connection.Open();
            string sql = "SELECT Nom FROM client";
            using (var command = new MySqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clients.Add(reader["Nom"].ToString());
                }
            }
        }

        return clients;
    }
}
