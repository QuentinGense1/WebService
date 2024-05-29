using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Classe principale de l'application
public class Program
{
    // Point d'entrée de l'application
    public static void Main(string[] args)
    {
        // Crée et exécute le host de l'application
        CreateHostBuilder(args).Build().Run();
    }

    // Méthode pour créer et configurer le builder de l'hôte
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            // Configure les paramètres par défaut du serveur web
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // Utilise la classe Startup pour configurer les services et le pipeline de requêtes
                webBuilder.UseStartup<Startup>();
            });
}

// Classe de démarrage pour configurer les services et le pipeline de requêtes
public class Startup
{
    // Constructeur de la classe Startup qui reçoit une instance de IConfiguration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Propriété pour accéder à la configuration de l'application
    public IConfiguration Configuration { get; }

    // Méthode pour configurer les services utilisés par l'application
    public void ConfigureServices(IServiceCollection services)
    {
        // Ajoute les services nécessaires pour les contrôleurs
        services.AddControllers();
    }

    // Méthode pour configurer le pipeline de traitement des requêtes HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Affiche la page d'exception pour les développeurs si l'environnement est en développement
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Force l'utilisation de HTTPS pour toutes les requêtes
        app.UseHttpsRedirection();

        // Ajoute la fonctionnalité de routage à l'application
        app.UseRouting();

        // Ajoute la fonctionnalité d'autorisation à l'application (peut être configurée plus en détail)
        app.UseAuthorization();

        // Configure les endpoints de l'application
        app.UseEndpoints(endpoints =>
        {
            // Mappe les contrôleurs pour qu'ils puissent gérer les requêtes entrantes
            endpoints.MapControllers();
        });
    }
}
