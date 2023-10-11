using System.DirectoryServices;
using System.Runtime.Versioning;
using Microsoft.Extensions.Configuration;

namespace WinFormDataAccess;

[SupportedOSPlatform("windows")]
public class DataAccessAD : IDataAccessAD
{
    private readonly IConfiguration configuration;

    public DataAccessAD(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DirectoryEntry ConnectToAD(string password)
    {
        var server = configuration["ActiveDirectory:Server"];
        var username = configuration["ActiveDirectory:Username"];

        try
        {
            DirectoryEntry ldapConnection = new DirectoryEntry(server, username, password);

            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;


            if (ldapConnection is not null)
            {
                return ldapConnection;
            }

            throw new InvalidOperationException("Can not connect to DirectoryEntry");
        }
        catch (Exception)
        {
            throw new InvalidOperationException();
        }
    }
}
