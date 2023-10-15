using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Runtime.Versioning;
using WinFormDataAccess;
using WinFormDataAccess.Querys;

namespace WinFormAD;

[SupportedOSPlatform("windows")]
public class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);


        // Build configuration
        var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddUserSecrets<Program>()
                                .AddJsonFile("appsettings.json")
                                .Build();

        // Create a service collection
        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);

        // Register your services
        services.AddSingleton<IDataAccessAD, DataAccessAD>();

        services.AddScoped<IEditUserPassword, EditUserPassword>();
        services.AddScoped<ISearchOU,  SearchOU>();
        services.AddScoped<ISearchUserAD, SearchUserAD>();

        services.AddTransient<Form1>();

        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the main form and pass the service provider to it
        var mainForm = serviceProvider.GetRequiredService<Form1>();

        Application.Run(mainForm);

    }
}