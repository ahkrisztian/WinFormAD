using Microsoft.Extensions.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;

namespace WinFormDataAccess;

[SupportedOSPlatform("windows")]
public class PrincipialContextDataAccess : IPrincipialContextDataAccess
{
    private readonly IConfiguration configuration;

    public PrincipialContextDataAccess(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public PrincipalContext principialContext(string password)
    {
        var server = configuration["ActiveDirectory:ServerPrincipal"];
        var username = configuration["ActiveDirectory:Username"];

        try
        {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, server, username, password);

            if (context != null)
            {
                return context;
            }

            throw new InvalidOperationException("Can not connect to PrincipalContext");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
