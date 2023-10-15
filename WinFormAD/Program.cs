using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Versioning;
using WinFormDataAccess;
using WinFormDataAccess.Querys;

namespace WinFormAD;

[SupportedOSPlatform("windows")]
internal class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Build configuration
        var configuration = new ConfigurationBuilder()
                                .AddUserSecrets<Program>()
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

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the main form and pass the service provider to it
        var mainForm = serviceProvider.GetRequiredService<Form1>();

        Application.Run(mainForm);

    }
}